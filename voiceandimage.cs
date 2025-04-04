using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
/*using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;*/

namespace New
{ 
    public class voiceandimage
    {
        public voiceandimage()
        {
           //This is for the Image/Logo
            string path_proj = AppDomain.CurrentDomain.BaseDirectory;

            string new_path_project = path_proj.Replace("bin\\Debug\\","");

            string full_path = Path.Combine(new_path_project, "CyberSecurity (2).jpg");

              Bitmap image = new Bitmap(full_path);
                image = new Bitmap(image, new Size(150, 180));

                for (int height = 0; height < image.Height; height++)
                {
                    for (int width = 0; width < image.Width; width++)
                    {
                    Color pixelColor = image.GetPixel(width, height);
                        int color = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                        char acsii_decor = color > 200 ? '.' : color > 160 ? '*' : color > 100 ? '0' : color > 60 ? '#' : '@';
                        Console.Write(acsii_decor);
                    }
                    Console.WriteLine();
                }
                
            //THIS IS FOR THE VOICE GREETING
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            // Console.WriteLine(full_location);
            //Replace the bin\Debug\ folder in the full_location

            string new_path = full_location.Replace("bin\\Debug\\", "");

            // Console.WriteLine(new_path);

            //THIS IS EXCEPTION HANDLING
            //Try and Catch
            try
            {//Combining the paths
                string full_pave = Path.Combine(new_path, "Greetings.wav");

                //Creating an instance for the SoundPlay class
                using (SoundPlayer play = new SoundPlayer(full_pave))
                {
                    //Play the File
                    play.PlaySync();
                }
            }
            catch (Exception error)
            {
                //changing the color to red to display the error message
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine(error.Message);
            }//End of Try&Catch

        }//End of Constructor

    }//End of Class

}//End of Namespace