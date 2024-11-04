using System.ComponentModel;
using System.Diagnostics;
using NetFwTypeLib;

namespace NetProcessControlWinForms
{
    public partial class Form1 : Form
    {
        private BindingList<ProcessInfo> processList = new BindingList<ProcessInfo>();
        private ProcessInfo selectedProcess = null;
        private bool isBlocked = false;

        public Form1()
        {
            InitializeComponent();
            LoadProcessList();
            SetupDataBindings();
        }

        private void LoadProcessList()
        {
            var processes = Process.GetProcesses();
            foreach (var proc in processes)
            {
                string processPath = null;
                try
                {
                    processPath = proc.MainModule.FileName;
                }
                catch
                {
                    // Chemin inaccessible
                }

                processList.Add(new ProcessInfo
                {
                    ProcessId = proc.Id,
                    ProcessName = proc.ProcessName,
                    ProcessPath = processPath
                });
            }

            processList = new BindingList<ProcessInfo>(processList.OrderBy(p => p.ProcessName).ToList());
        }

        private void SetupDataBindings()
        {
            listBoxProcesses.DataSource = processList;
            listBoxProcesses.DisplayMember = "DisplayName";
            listBoxProcesses.ValueMember = "ProcessId";

            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            listBoxProcesses.SelectedIndexChanged += ListBoxProcesses_SelectedIndexChanged;
            buttonToggle.Click += ButtonToggle_Click;
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string filter = textBoxSearch.Text.ToLower();
            listBoxProcesses.DataSource = new BindingList<ProcessInfo>(
                processList.Where(p => p.ProcessName.ToLower().Contains(filter)).ToList());
        }

        private void ListBoxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProcess = (ProcessInfo)listBoxProcesses.SelectedItem;
            if (selectedProcess != null)
            {
                labelSelectedProcess.Text = $"Processus sélectionné : {selectedProcess.ProcessName} (PID: {selectedProcess.ProcessId})";
                UpdateToggleButton();
                labelStatus.Text = "";
            }
        }

        private void ButtonToggle_Click(object sender, EventArgs e)
        {
            if (selectedProcess == null)
            {
                labelStatus.Text = "Veuillez sélectionner un processus.";
                return;
            }

            string ruleName = $"NetBlock_{selectedProcess.ProcessName}_{selectedProcess.ProcessId}";

            if (!isBlocked)
            {
                if (BlockProcessNetwork(selectedProcess.ProcessPath, ruleName))
                {
                    isBlocked = true;
                    UpdateToggleButton();
                    labelStatus.Text = $"La connexion internet pour {selectedProcess.ProcessName} a été coupée.";
                }
                else
                {
                    labelStatus.Text = "Impossible de bloquer la connexion.";
                }
            }
            else
            {
                if (UnblockProcessNetwork(ruleName))
                {
                    isBlocked = false;
                    UpdateToggleButton();
                    labelStatus.Text = $"La connexion internet pour {selectedProcess.ProcessName} a été rétablie.";
                }
                else
                {
                    labelStatus.Text = "Impossible de rétablir la connexion.";
                }
            }
        }

        private void UpdateToggleButton()
        {
            if (selectedProcess == null)
            {
                buttonToggle.Text = "OFF";
                buttonToggle.Enabled = false;
                return;
            }

            string ruleName = $"NetBlock_{selectedProcess.ProcessName}_{selectedProcess.ProcessId}";
            isBlocked = RuleExists(ruleName);
            buttonToggle.Text = isBlocked ? "ON" : "OFF";
            buttonToggle.Enabled = true;
        }

        private bool BlockProcessNetwork(string processPath, string ruleName)
        {
            if (string.IsNullOrEmpty(processPath))
            {
                labelStatus.Text = "Chemin du processus inaccessible.";
                return false;
            }

            try
            {
                INetFwPolicy2 firewallPolicy = GetFirewallPolicy();

                if (RuleExists(ruleName))
                {
                    return true;
                }

                INetFwRule newRule = CreateFirewallRule(processPath, ruleName);
                firewallPolicy.Rules.Add(newRule);

                return true;
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Erreur : {ex.Message}";
                return false;
            }
        }

        private bool UnblockProcessNetwork(string ruleName)
        {
            try
            {
                INetFwPolicy2 firewallPolicy = GetFirewallPolicy();

                if (RuleExists(ruleName))
                {
                    firewallPolicy.Rules.Remove(ruleName);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"Erreur : {ex.Message}";
                return false;
            }
        }

        private bool RuleExists(string ruleName)
        {
            INetFwPolicy2 firewallPolicy = GetFirewallPolicy();
            foreach (INetFwRule rule in firewallPolicy.Rules)
            {
                if (rule.Name == ruleName)
                {
                    return true;
                }
            }
            return false;
        }

        private INetFwPolicy2 GetFirewallPolicy()
        {
            return (INetFwPolicy2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
        }

        private INetFwRule CreateFirewallRule(string processPath, string ruleName)
        {
            INetFwRule newRule = (INetFwRule)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwRule"));

            newRule.Name = ruleName;
            newRule.ApplicationName = processPath;
            newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            newRule.Enabled = true;
            newRule.InterfaceTypes = "All";

            return newRule;
        }
    }

    public class ProcessInfo
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string ProcessPath { get; set; }

        public string DisplayName
        {
            get
            {
                string pathInfo = string.IsNullOrEmpty(ProcessPath) ? " (Chemin inaccessible)" : "";
                return $"{ProcessName} (PID: {ProcessId}){pathInfo}";
            }
        }
    }
}