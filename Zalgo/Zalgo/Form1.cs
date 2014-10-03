using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zalgo
{
    public partial class Form1 : Form
    {
#region symbols
        //those go UP
	char[] zalgo_up = new char[]
    {
		'\u030d', /*     ̍     */		'\u030e', /*     ̎     */		'\u0304', /*     ̄     */		'\u0305', /*     ̅     */
		'\u033f', /*     ̿     */		'\u0311', /*     ̑     */		'\u0306', /*     ̆     */		'\u0310', /*     ̐     */
		'\u0352', /*     ͒     */		'\u0357', /*     ͗     */		'\u0351', /*     ͑     */		'\u0307', /*     ̇     */
	    '\u0308', /*     ̈     */		'\u030a', /*     ̊     */		'\u0342', /*     ͂     */		'\u0343', /*     ̓     */
		'\u0344', /*     ̈́     */		'\u034a', /*     ͊     */		'\u034b', /*     ͋     */		'\u034c', /*     ͌     */
		'\u0303', /*     ̃     */		'\u0302', /*     ̂     */		'\u030c', /*     ̌     */		'\u0350', /*     ͐     */
		'\u0300', /*     ̀     */		'\u0301', /*     ́     */		'\u030b', /*     ̋     */		'\u030f', /*     ̏     */
		'\u0312', /*     ̒     */		'\u0313', /*     ̓     */		'\u0314', /*     ̔     */		'\u033d', /*     ̽     */
		'\u0309', /*     ̉     */		'\u035b', /*     ͛     */        '\u0346', /*     ͆     */		'\u031a' /*     ̚     */
	};

	//those go DOWN
	char[] zalgo_down = new char[]
    {
		'\u0316', /*     ̖     */		'\u0317', /*     ̗     */		'\u0318', /*     ̘     */		'\u0319', /*     ̙     */
		'\u031c', /*     ̜     */		'\u031d', /*     ̝     */		'\u031e', /*     ̞     */		'\u031f', /*     ̟     */
		'\u0320', /*     ̠     */		'\u0324', /*     ̤     */		'\u0325', /*     ̥     */		'\u0326', /*     ̦     */
		'\u0329', /*     ̩     */		'\u032a', /*     ̪     */		'\u032b', /*     ̫     */		'\u032c', /*     ̬     */
		'\u032d', /*     ̭     */		'\u032e', /*     ̮     */		'\u032f', /*     ̯     */		'\u0330', /*     ̰     */
		'\u0331', /*     ̱     */		'\u0332', /*     ̲     */		'\u0333', /*     ̳     */		'\u0339', /*     ̹     */
		'\u033a', /*     ̺     */		'\u033b', /*     ̻     */		'\u033c', /*     ̼     */		'\u0345', /*     ͅ     */
		'\u0347', /*     ͇     */		'\u0348', /*     ͈     */		'\u0349', /*     ͉     */		'\u034d', /*     ͍     */
		'\u034e', /*     ͎     */		'\u0353', /*     ͓     */		'\u0354', /*     ͔     */		'\u0355', /*     ͕     */
		'\u0356', /*     ͖     */		'\u0359', /*     ͙     */		'\u035a', /*     ͚     */		'\u0323' /*     ̣     */
	};

	//those always stay in the middle
	char[] zalgo_mid = new char[]
    {
		'\u0315', /*     ̕     */		'\u031b', /*     ̛     */		'\u0340', /*     ̀     */		'\u0341', /*     ́     */
		'\u0358', /*     ͘     */		'\u0321', /*     ̡     */		'\u0322', /*     ̢     */		'\u0327', /*     ̧     */
		'\u0328', /*     ̨     */		'\u0334', /*     ̴     */		'\u0335', /*     ̵     */		'\u0336', /*     ̶     */
		'\u034f', /*     ͏     */		'\u035c', /*     ͜     */		'\u035d', /*     ͝     */		'\u035e', /*     ͞     */
		'\u035f', /*     ͟     */		'\u0360', /*     ͠     */		'\u0362', /*     ͢     */		'\u0338', /*     ̸     */
		'\u0337', /*     ̷     */		'\u0361', /*     ͡     */		'\u0489' /*     ҉_     */
	};
#endregion
    Random r;
        public Form1()
        {
            InitializeComponent();
            r = new Random(DateTime.Now.Millisecond);
        }

        bool is_zalgo_char(char c)
	    {
		    int i;
		    for(i=0; i<zalgo_up.Length; i++)
		    	if(c == zalgo_up[i])
		    		return true;
		    
            for(i=0; i<zalgo_down.Length; i++)
		    	if(c == zalgo_down[i])
		    		return true;
		    
            for(i=0; i<zalgo_mid.Length; i++)
		    	if(c == zalgo_mid[i])
		    		return true;

		    return false;
        }

        char rand_zalgo(char[] c)
	    {
            return c[r.Next(c.Length)];
	    }

        private void button1_Click(object sender, EventArgs e)
        {
		    var txt = textBox1.Text;
		    var newtxt = "";
            
		    for(var i=0; i<txt.Length; i++)
		    {
			    if(is_zalgo_char(txt.Substring(i, 1)[0]))
				    continue;
			    int num_up, num_mid, num_down;

			    //add the normal character
                if (r.Next(2) == 1)
                {
                    newtxt += " ";
                    i--;
                }
                else
                {
                    newtxt += txt.Substring(i, 1);
                }
#region options
			    //zalgo_opt_mini
			    {
			    	num_up = r.Next(8);
			    	num_mid = r.Next(2);
			    	num_down = r.Next(8);
			    }
			    //zalgo_opt_normal
			    {
			    	num_up = r.Next(16) / 2 + 1;
			    	num_mid = r.Next(6) / 2;
			    	num_down = r.Next(16) / 2 + 1;
			    }
			    //maxi
                //{
                //    num_up = r.Next(64) / 4 + 3;
                //    num_mid = r.Next(16) / 4 + 1;
                //    num_down = r.Next(64) / 4 + 3;
                //}
#endregion


                //zalgo_opt_up
                for (var j = 0; j < num_up; j++)
                    newtxt += rand_zalgo(zalgo_up);
                //zalgo_opt_mid
                for (var j = 0; j < num_mid; j++)
                    newtxt += rand_zalgo(zalgo_mid);
                //zalgo_opt_down
                for (var j = 0; j < num_down; j++)
                    newtxt += rand_zalgo(zalgo_down);

		    }

		    //result is in nextxt, display that

		    textBox2.Text = Environment.NewLine + newtxt;


            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            int t = 0;
            for (int i = 0; i < newtxt.Length; i++)
            {
                if (is_zalgo_char(newtxt.Substring(i, 1)[0]))
                {
                    gr.DrawString(newtxt.Substring(i, 1), textBox2.Font, new SolidBrush(Color.Black),
                        new PointF(t, 50));
                }
                else
                {
                    
                    gr.DrawString(newtxt.Substring(i, 1), textBox2.Font, new SolidBrush(Color.Black),
                        new PointF(t, r.Next(60)+20));
                    if (newtxt.Substring(i, 1)[0] == ' ')
                        t += 15;
                    else
                        t += 20;
                }
            }
		    //done
	    }
        
    }
}
