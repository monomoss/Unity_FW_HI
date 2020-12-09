using UnityEngine;
using UnityEngine.UI;

namespace HI
{
	public class HiText_NumberUpdate : MonoBehaviour
	{
		private Text TXT_textField = null;

		private bool isNumberUpdate = false;
		private int updateFrame = 0;
		private double currentNum = 0;
		private double startNum = 0;
		private double lastNum = 0;
		private double gapNum = 0;

		void Awake()
		{
			TXT_textField = this.GetComponent<Text>();
			TXT_textField.text = "0";
		}

		void Update()
		{
			if (isNumberUpdate == false)
			{
				return;
			}
			currentNum += gapNum;
			TextUpdate(currentNum);
		}

		public void SetNumber(double _num)
		{
			TXT_textField.text = _num.ToString();
		}
		
		public void NumberUpdate(double _num, int _updateFrame = 100)
		{
			updateFrame = _updateFrame;
			lastNum = _num;
			currentNum = startNum;
			gapNum = GetGap();
			isNumberUpdate = true;
		}

		private long GetGap()
		{
			double _num = (lastNum - startNum) / updateFrame;
			if ((int)_num == 0)
			{
				if (_num > 0)
				{
					_num = 1;
				}
				if (_num < 0)
				{
					_num = -1;
				}
			}

			_num = (int)_num;
			int _length = _num.ToString().Length;
			string _str = "";
			for (int i=0; i<_length-1; i++)
			{
				if (i%2 == 0)
				{
					_str += "2";
				}
				else
				{
					_str += "1";
				}
			}
			_str = _num.ToString().Substring(0,1)+ _str;
			return int.Parse(_str);
		}

		private void TextUpdate(double _num)
		{
			if (gapNum >= 0)
			{
				if (_num >= lastNum)
				{
					isNumberUpdate = false;
					_num = lastNum;
				}
			}
			else
			{
				if (_num <= lastNum)
				{
					isNumberUpdate = false;
					_num = lastNum;
				}
			}
			startNum = _num;
			TXT_textField.text = _num.ToString();
		}
	}
}
