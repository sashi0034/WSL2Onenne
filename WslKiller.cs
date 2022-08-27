using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WSL2Onenne
{
    public struct Unit
    {
        public static readonly Unit Default = new Unit();
    }

    public class WslKiller
    {
        private readonly CommandView _commandViewRef;
        private readonly IntTextView _textNumExecuteRef;
        private CancellationTokenSource _taskCancellation = new();

        private const string LiteralWslFileName = "wsl.exe";
        private const string LiteralWslArgListRunning = "--list --running";
        private const string LiteralWslArgShutdon = "--shutdown";

        public WslKiller(CommandView commandViewRef, IntTextView textNumExecuteRef)
        {
            _commandViewRef = commandViewRef;
            _textNumExecuteRef = textNumExecuteRef;
            init();
        }

        private void init()
        {
            _commandViewRef.AddEvent(startProcess, stopProcess);
        }

        private async void startProcess()
        {
            _taskCancellation = new CancellationTokenSource();
            _textNumExecuteRef.Value = 0;

            while (true)
            {
                if (_taskCancellation.IsCancellationRequested) return;

                tryKillWsl();

                const int delayDuration = 1000 * 5;
                await Task.Delay(delayDuration);
            }
        }

        private void tryKillWsl()
        {
            if (!isRunningWsl()) return;

            killWsl();

            _textNumExecuteRef.Value++;
        }

        private bool isRunningWsl()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(LiteralWslFileName, LiteralWslArgListRunning);

            // ウィンドウを表示しない
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;

            // 標準出力を取得できるようにする
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.StandardOutputEncoding = Encoding.Unicode;

            Process process = Process.Start(processStartInfo);
            process.WaitForExit();
            string standardOutput = process.StandardOutput.ReadToEnd();

            process.Close();

            int numLine = standardOutput.Count(c => c == '\n');

            const int numLineWhenNotRunning = 1;

            return numLine > numLineWhenNotRunning;
        }

        private void killWsl()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(LiteralWslFileName, LiteralWslArgShutdon);

            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;

            Process process = Process.Start(processStartInfo);
            process.WaitForExit();
            process.Close();
        }


        private void stopProcess()
        {
            _taskCancellation.Cancel();
            MessageBox.Show("プロセスを終了しました", "おねんね終了");
        }

    }
}
