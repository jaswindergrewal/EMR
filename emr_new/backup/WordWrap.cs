using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

public class WordWrap
{
	#region Public Methods
	public static string Wrap(string originalText, int maxWidth)
	{
		return Wrap(originalText, maxWidth, "");
	} /* Wrap */

	public static string Wrap(string originalText, int maxWidth, string preFix)
	{
		string[] wrappedText = Wrapper(originalText, maxWidth);

		string wrappedBlock = "";
		foreach (string textLine in wrappedText)
		{
			wrappedBlock = String.Format("{0}\n{1}{2}", wrappedBlock, preFix, textLine);
		}
		wrappedBlock = wrappedBlock.Substring(1);
		return wrappedBlock.Replace("\n","<br />");
	} /* Wrap */
	#endregion

	#region Private Methods
	private static string[] Wrapper(string originalText, int maxWidth)
	{
		originalText = originalText.Replace("<br/>", "\r\n");
		originalText = originalText.Replace("<br>", "\r\n");
		originalText = originalText.Replace("<br />", "\r\n");
		originalText = originalText.Replace("\r\n", "\n");
		originalText = originalText.Replace("\r", "\n");
		originalText = originalText.Replace("\t", " ");
		string[] textParagraphs = originalText.Split('\n');
		ArrayList textLines = new ArrayList();

		for (int i = 0; i < textParagraphs.Length; i++)
		{
			if (textParagraphs[i].Length <= maxWidth)
			{
				// Block of text is smaller then width, add it
				textLines.Add(textParagraphs[i]);
			}
			else
			{
				// Block of text is longer, break it up in seperate lines
				string[] pLines = BreakLines(textParagraphs[i], maxWidth);
				for (int j = 0; j < pLines.Length; j++)
				{
					textLines.Add(pLines[j]);
				}
			}
		}

		string[] wrappedText = new string[textLines.Count];
		textLines.CopyTo(wrappedText, 0);
		return wrappedText;
	} /* Wrapper */

	private static string[] BreakLines(string originalText, int maxWidth)
	{
		string[] textWords = originalText.Split(' ');
		int wordIndex = 0;
		string tmpLine = "";
		ArrayList textLines = new ArrayList();

		while (wordIndex < textWords.Length)
		{
			if (textWords[wordIndex] == "")
			{
				wordIndex++;
			}
			else
			{
				string backupLine = tmpLine;
				if (tmpLine == "")
				{
					tmpLine = textWords[wordIndex];
				}
				else
				{
					tmpLine = tmpLine + " " + textWords[wordIndex];
				}

				if (tmpLine.Length <= maxWidth)
				{
					wordIndex++;
					// If our line is still small enough, but we don't have anymore words
					// add the line to the collection
					if (wordIndex == textWords.Length)
					{
						textLines.Add(tmpLine);
					}
				}
				else
				{
					// Our line is too long, add the previous line to the collection
					// and reset the line, the word causing the 'overflow' will be
					// the first word of the new line
					textLines.Add(backupLine);
					tmpLine = "";
				}
			}
		}
		string[] textLinesStr = new string[textLines.Count];
		textLines.CopyTo(textLinesStr, 0);
		return textLinesStr;
	} /* BreakLines */
	#endregion
} /* WordWrap */
