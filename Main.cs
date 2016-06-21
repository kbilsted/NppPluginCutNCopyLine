using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
	class Main
	{
		internal const string PluginName = "CutNCopyLine";

		public static void OnNotification(ScNotification notification)
		{
		}

		internal static void CommandMenuInit()
		{
			PluginBase.SetCommand(0, "&Copy selection or line", CopySelectionOrLine, new ShortcutKey(true, false, false, Keys.C));
			PluginBase.SetCommand(0, "C&ut selection or line", CutSelectionOrLine, new ShortcutKey(true, false, false, Keys.X));

			PluginBase.SetCommand(0, "&About "+PluginName, ShowAbout, new ShortcutKey(false, false, false, Keys.None));
		}

		internal static void SetToolBarIcon()
		{
		}

		internal static void PluginCleanUp()
		{
		}

		private static void CopySelectionOrLine()
		{
			var scintilla = new ScintillaGateway(PluginBase.GetCurrentScintilla());
			if (scintilla.GetSelectionLength() != 0)
				scintilla.Copy();
			else
				scintilla.CopyAllowLine();
		}

		private static void CutSelectionOrLine()
		{
			var scintilla = new ScintillaGateway(PluginBase.GetCurrentScintilla());
			if (scintilla.GetSelectionLength() != 0)
				scintilla.Cut();
			else
			{
				scintilla.CopyAllowLine();
				scintilla.LineDelete();
			}
		}

		private static void ShowAbout()
		{
			var message = @"Version: 1.01

This plugin changes how ctrl+c and ctrl+x work. 
If no selection is made, the commands operate on the current line.
This means you can easily operate on lines - just don't select anything 
before copying or cutting.

License: This is freeware (Apache v2.0 license).

Author: Kasper B. Graversen 2016-

Website: https://github.com/kbilsted/nppPluginCutNCopyLine";
			var title = PluginName;
			MessageBox.Show(message, title, MessageBoxButtons.OK);
		}
	}
}
