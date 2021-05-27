using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Card_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        static int bitis; //kaç eşleştirme yaptığımızı tutmak için atadığımız değişken
        static string[] secilenler = new string[2]; //2 kartın eşleşmesini kontrol edeceğimizden bu kartları bir dizi içine aktaracağız. Bu da aktaracağımız dizi.
        static int süre; // oyunda kalan süremizi kullanıcıya göstermek için atadığımız değişken
        static double oyunPuanı = 0; // kullanıcının puan değişkeni

        PictureBox oncekiButon; // kart eşleştirilmesi yapılırken ilk tıklanan butonu tutabilmemiz için atadığımız pictureBox nesnesi
        SoundPlayer player = new SoundPlayer(); //oyun içi seslerin çalması için gerekli atama.

        private void button1_Click(object sender, EventArgs e)
        {
            //Oyun kurma butonuna tıklandığında onceden kurulmuş bir oyun alanı olma ihtimalini göz onunde bulundurarak tum değişkenlerimizi temizliyoruz.
            //Kullanıcının seçtiği oyun alanını oluşturmak için gerekli fonksiyonları çağırıyoruz.
            //Oyun Alanı seçilmeden butona tıklanırsa hata mesajı gösteriyoruz.

            
            secilenler[0] = null;
            secilenler[1] = null; 
            panel1.Enabled = true;
            oyunPuanı = 0;
            label2.Text = "Oyun Puanı : " + oyunPuanı.ToString();
            süre = 0;
            timer1.Stop();
            panel1.Controls.Clear();


            if (comboBox1.Text == "2x5")
                x2();
            else if (comboBox1.Text == "3x6")
                x3();
            else if (comboBox1.Text == "4x7")
                x4();
            else
                MessageBox.Show("Oyun Alanı Seçmediniz...", "HATA");

        }
        static string[,] oyunAlanı2x5 = new string[2, 5];
        void x2()
        {
            //2x5 lik oyunumuzu burada oluşturuyoruz.


            oyunPuanı = 0;
            süre = 120; // süremizi 120 saniye olarak ayarlıyoruz
            MessageBox.Show("2x5 Oyun Alanınız Oluşturuldu. Kart gözlem süresi 5 saniye Toplam Süreniz 2 dakika.", "BOL ŞANS");
            timer1.Start();
            bitis = 0;
            //2 satır 5 sutündan oluşan oyun alanı

           //oyun alanımız 2 satır 5 sutun olacağızı için 2 boyutlu dizimize bu değerleri veriyoruz.
            Random random = new Random(); //random nesnesi ile oyun alanını rastgele oluşturacağız.

            string[] meslekler = { "1", "2", "3", "4", "5" };

            string[] secim1 = new string[10]; //rastgele oyun alanı kuracağımız için aynı karttan maksimum 2 adet olma durumunu kotrol etmemiz gerekiyor. 
            string[] secim2 = new string[10]; // bu diziler sayesinde onu kontrol edeceğiz


            for (int i = 0; i < 10; i++) //10 adet kart seçilecek 2x5 olduğu için
            {


                while (true)
                {
                    int satır = random.Next(0, 2); // kartın rastgele yerleştirileceği satır
                    int sutun = random.Next(0, 5); // kartın rastgele yerleştirileceği sutun

                    if (oyunAlanı2x5[satır, sutun] == null) // rastgele seçim tamamlandıktan sonra bu yerin boş olup olmadığını kontrol ediyoruz
                    {


                        while (true)
                        {

                            int meslek = random.Next(0, 5); // seçilen bölgeye rastgele bir kart atıyoruz
                            if (secim1.Contains(meslekler[meslek]) == false) // bu kartın 1.sinin daha once secilip secilmedigine bakıyoruz  
                            {
                                oyunAlanı2x5[satır, sutun] = meslekler[meslek]; //kartı bölgeye koyuyooruz
                                secim1[i] = meslekler[meslek]; //kartın secildiğini dizimize aktarıyoruz
                                break; //while döngüsünden çıkıyoruz
                            }

                            else
                            {
                                if (secim2.Contains(meslekler[meslek]) == false) //kartın 2. seçimini kontrol ediyoruz eğer yapılmadıysa
                                {
                                    oyunAlanı2x5[satır, sutun] = meslekler[meslek]; //kartı bölgeye koyuyooruz
                                    secim2[i] = meslekler[meslek];//kartın secildiğini dizimize aktarıyoruz
                                    break;//while döngüsünden çıkıyoruz
                                }
                            }

                        }



                        break; // eğer seçilen alan doluysa tekrardan rastgele aan belirlenmesi için for döngmüzden çıkıyoruz
                    }

                }



            }

            for (int i = 0; i < 2; i++) //kullanıcının oyuna hazırlamnması için görselleri açıyoruz.
            {
                for (int j = 0; j < 5; j++)
                {
                    PictureBox dugme = new PictureBox();

                    dugme.Top = (350 * i) + 50; 
                    dugme.Left = 230 * j + 20;
                    dugme.Width = 200;
                    dugme.Height = 300;


                    dugme.Image = Image.FromFile(@"C:\cg\2x5\" + oyunAlanı2x5[i, j] + ".jpg");
                    dugme.SizeMode = PictureBoxSizeMode.StretchImage;
                    dugme.Name = oyunAlanı2x5[i, j];

                    dugme.Click += new EventHandler(click);

                    panel1.Controls.Add(dugme);
                    

                }

            }

           
            panel1.Enabled = false;
            timer2.Start(); //timer ile sayım yapıp resimleri 5 saniye gösteriyoruz.



          




        }

        static string[,] oyunAlanı3x6 = new string[3, 6];
        //x2 fonksiyonuyla aynı şeyler
        void x3()
        {
            oyunPuanı = 0;
            süre = 180;
            MessageBox.Show("3x6 Oyun Alanınız Oluşturuldu. Kart gözlem süresi 8 saniye Toplam Süreniz 3 dakika.", "BOL ŞANS");
            timer1.Start();
            bitis = 0;
            //2 satır 5 sutündan oluşan oyun alanı

           
            Random random = new Random();

            string[] meslekler = { "1","2","3","4","5","6","7","8","9" };

            string[] secim1 = new string[18];
            string[] secim2 = new string[18];


            for (int i = 0; i < 18; i++)
            {


                while (true)
                {
                    int satır = random.Next(0, 3);
                    int sutun = random.Next(0, 6);

                    if (oyunAlanı3x6[satır, sutun] == null)
                    {


                        while (true)
                        {

                            int meslek = random.Next(0, 9);
                            if (secim1.Contains(meslekler[meslek]) == false)
                            {
                                oyunAlanı3x6[satır, sutun] = meslekler[meslek];
                                secim1[i] = meslekler[meslek];
                                break;
                            }

                            else
                            {
                                if (secim2.Contains(meslekler[meslek]) == false)
                                {
                                    oyunAlanı3x6[satır, sutun] = meslekler[meslek];
                                    secim2[i] = meslekler[meslek];
                                    break;
                                }
                            }

                        }



                        break;
                    }

                }



            }


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    PictureBox dugme = new PictureBox();

                    dugme.Top = (235 * i) + 50;
                    dugme.Left = 200 * j + 20;
                    dugme.Width = 150;
                    dugme.Height = 200;

                    dugme.Image = Image.FromFile(@"C:\cg\3x6\" + oyunAlanı3x6[i, j] + ".jpg");
                    dugme.SizeMode = PictureBoxSizeMode.StretchImage;
                    dugme.Name = oyunAlanı3x6[i, j];

                    dugme.Click += new EventHandler(click);

                    panel1.Controls.Add(dugme);

                }

            }

            panel1.Enabled = false;
            timer3.Start();


        }


        static string[,] oyunAlanı4x7 = new string[4, 7];
        void x4()
        {
            oyunPuanı = 0;
            süre = 240;
            MessageBox.Show("4x7 Oyun Alanınız Oluşturuldu. Kart gözlem süresi 10 saniye Toplam Süreniz 4 dakika.", "BOL ŞANS");
            timer1.Start();
            bitis = 0;
           
            Random random = new Random();

            string[] meslekler = { "1","2","3","4","5","6","7","8","9","10","11","12","13","14"};

            string[] secim1 = new string[28];
            string[] secim2 = new string[28];


            for (int i = 0; i < 28; i++)
            {


                while (true)
                {
                    int satır = random.Next(0, 4);
                    int sutun = random.Next(0, 7);

                    if (oyunAlanı4x7[satır, sutun] == null)
                    {


                        while (true)
                        {

                            int meslek = random.Next(0, 14);
                            if (secim1.Contains(meslekler[meslek]) == false)
                            {
                                oyunAlanı4x7[satır, sutun] = meslekler[meslek];
                                secim1[i] = meslekler[meslek];
                                break;
                            }

                            else
                            {
                                if (secim2.Contains(meslekler[meslek]) == false)
                                {
                                    oyunAlanı4x7[satır, sutun] = meslekler[meslek];
                                    secim2[i] = meslekler[meslek];
                                    break;
                                }
                            }

                        }



                        break;
                    }

                }



            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    PictureBox dugme = new PictureBox();

                    dugme.Top = (195 * i) + 10;
                    dugme.Left = 170 * j + 20;
                    dugme.Width = 120;
                    dugme.Height = 170;


                    dugme.Image = Image.FromFile(@"C:\cg\4x7\"+ oyunAlanı4x7[i, j]+".jpg");
                    dugme.SizeMode = PictureBoxSizeMode.StretchImage;
                    dugme.Name = oyunAlanı4x7[i, j];

                    dugme.Click += new EventHandler(click);

                    panel1.Controls.Add(dugme);

                }

            }



            panel1.Enabled = false;
            timer4.Start();



        }



       

        private void click(object sender, System.EventArgs e)
        {
            
            
            PictureBox tıklama1 = (sender as PictureBox); //tıklanan nesnemizi alıyoruz

            if(secilenler[0] == null)//tıklama kontrol dizimizin ilki boş ise 
            {
                tıklama1.Enabled = false; //tekrar tıklanmaması için nesneyi ktliyoruz
                secilenler[0] = tıklama1.Name; //1. tıklamayı kaydediyoruz

                //oyun alanımıza göre nesnenin resmini gerekli klasorden çekip kullanıcıya gösteriyoruz.
                if(comboBox1.Text == "2x5")
                    tıklama1.Image = Image.FromFile(@"C:\cg\2x5\" + tıklama1.Name+".jpg");
                if (comboBox1.Text == "3x6")
                    tıklama1.Image = Image.FromFile(@"C:\cg\3x6\" + tıklama1.Name + ".jpg");
                if (comboBox1.Text == "4x7")
                    tıklama1.Image = Image.FromFile(@"C:\cg\4x7\" + tıklama1.Name + ".jpg");

                oncekiButon = tıklama1; // 2. tıklamayı da kontrol edebilmek için bunu kontrol değiştkenimize eşitliyoruz.
            }

            else
            {
                if(secilenler[1] == null)
                { //tıklama kontrol dizimizin ilkine tıklanmış ikincisi boş ise 

                    tıklama1.Enabled = false;   // tekrar tıklanmaması için kitliyoruz
                    secilenler[1] = tıklama1.Name; //2. tıklamayı kaydediyoruz

                    //oyun alanı seçimine göre gerekli resmi kullanıcıya gösteriyoruz
                    if (comboBox1.Text == "2x5")
                        tıklama1.Image = Image.FromFile(@"C:\cg\2x5\" + tıklama1.Name + ".jpg");
                    if (comboBox1.Text == "3x6")
                        tıklama1.Image = Image.FromFile(@"C:\cg\3x6\" + tıklama1.Name + ".jpg");
                    if (comboBox1.Text == "4x7")
                        tıklama1.Image = Image.FromFile(@"C:\cg\4x7\" + tıklama1.Name + ".jpg");

                    if (secilenler[0] == secilenler[1]) // eğer iki tıklama da aynıysa yani eşleştrime başarılıysa
                    {
                        player.Stop();
                        player.SoundLocation = @"C:\cg\doğru.wav"; // doğru cevap müziğimizi çalıyoruz.
                        player.Play();
                        Thread.Sleep(100);
                        MessageBox.Show("Eşleştirmen Doğru!! Aynen Böyle Devam","Bravo"); //kullanıcıya uyarısını gönderiyoruz.

                       
                        bitis += 1; // eşleştirme sayımızı 1 arttırıyoruz
                        secilenler[0] = null; //tıklanma kontrol dizimizi boşaltıoruz ki diğer tıklanmaları da kontrol edebilelim
                        secilenler[1] = null;

                        oyunPuanı = oyunPuanı + 100; // doğru eşleşme için kullanıcıya +100 puan veriyoruz
                        label2.Text = "Oyun Puanı : " + oyunPuanı.ToString(); //kullanıcıya puanını görsel olarak gösteriyoruz.


                        //Oyun Alanımıza göre oyunun bitmesi için gerekli Eşletirme sayımıza ulaşıp ulaşmadığımızı kontrol ediyoruz
                        if (comboBox1.Text == "2x5" && bitis == 5)
                        {
                            player.Stop();
                            player.SoundLocation = @"C:\cg\bitiş.wav"; // bitiş müziğimizi çalıyoruz.
                            player.Play();
                            timer1.Stop(); // zaman sayacımızı durduruyoruz.

                            oyunPuanı = oyunPuanı + (1.5 * süre); // kalan sürenin 1.5 katını oyun puanına ekliyoruz.
                            label2.Text = "Oyun Puanı : " + oyunPuanı.ToString(); //ullanıcıya puanını gösteriyoruz.
                            MessageBox.Show("Tebrikler. Süren Dolmadan Oyunu Tamamladın!!!\nPuanın : "+oyunPuanı,"OYUN BİTTİ");//oyunun bittiğini kullanıcıya söylüyoruz

                            panel1.Enabled = false;//oyun alanını kitliyoruz.
                        }

                        if (comboBox1.Text == "3x6" && bitis == 9)
                        {
                            player.Stop();
                            player.SoundLocation = @"C:\cg\bitiş.wav";
                            player.Play();
                            timer1.Stop();
                            label2.Text = "Oyun Puanı : " + oyunPuanı.ToString();
                            MessageBox.Show("Tebrikler. Süren Dolmadan Oyunu Tamamladın!!!\nPuanın : " + oyunPuanı, "OYUN BİTTİ");
                            panel1.Enabled = false;
                        }

                        if (comboBox1.Text == "4x7" && bitis == 14)
                        {
                            player.Stop();
                            player.SoundLocation = @"C:\cg\bitiş.wav";
                            player.Play();
                            timer1.Stop();
                            label2.Text = "Oyun Puanı : " + oyunPuanı.ToString();
                            MessageBox.Show("Tebrikler. Süren Dolmadan Oyunu Tamamladın!!!\nPuanın : " + oyunPuanı, "OYUN BİTTİ");
                            panel1.Enabled = false;
                        }



                    }
                    else // eğer eşleştirme doğru değilse
                    {
                        player.Stop();
                        player.SoundLocation = @"C:\cg\yanlış.wav"; //yanlış sesi çalıyoruz.
                        player.Play();
                        MessageBox.Show("Maalesef Yanlış, Canını Sıkma Odaklan!","Yanlış"); // kullanıcıya uyarı gönderiyoruz
                     

                        oncekiButon.Image = Image.FromFile(@"C:\cg\asd.jpg"); // tıklanan kartları tekrar kapatıyoruz yani soru işareti resmimizi koyuyoruz
                        tıklama1.Image = Image.FromFile(@"C:\cg\asd.jpg");
                        
                        secilenler[0] = null; //seçim dizimizi temizliyoruz
                        secilenler[1] = null;
                        oncekiButon.Enabled = true; //seçim esnasında kitlediğimiz kartları kilitlemiştik burada kilitleri kaldırıyoruz.
                        tıklama1.Enabled = true;

                        oyunPuanı = oyunPuanı - 30; //yanlış seçimden oturu kullanıcıdan puan kırıyoruz.
                        label2.Text = "Oyun Puanı : " + oyunPuanı.ToString();//kullancıya puanını gösteriyoruz.
                        
                    }
                
                }
            }


        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            süre -= 1;//süremizi 1 saniye azaltarak kullanıcıya gösteriyoruz
            label3.Text = "Kalan Süre : " + süre.ToString() + " Saniye";
         

            if(süre == 0) // eğer süre biterse
            {
                timer1.Stop(); // sayacı durduruypruz
                panel1.Enabled = false; //oyun alanını kitliyoruz
                player.Stop();
                player.SoundLocation = @"C:\cg\elenme.wav"; // süre bitimi müziğini çalıyoruz..
                player.Play();
                MessageBox.Show("Verilen Süre içerisinde oyunu bitiremedin :( Bence Tekrar Denemelisin!\n\n Puanın : "+oyunPuanı, "Süren Doldu!!"); // şuana kadarki puanını kullanıcıya göstererek puanının bittiğini söylüyoruz.
            }
           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            //oyun alanının temizleyip kapalı kartları yerleştiriyoruz.

            panel1.Enabled = true;
            timer2.Stop();
            panel1.Controls.Clear();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    PictureBox dugme = new PictureBox(); //resim göstereceğimizden picturebox nesnesi oluşturuyoruz

                    dugme.Top = (350 * i) + 50; // nesnemizi konumlandırıyoruz
                    dugme.Left = 230 * j + 20;
                    dugme.Width = 200;
                    dugme.Height = 300;


                    dugme.Image = Image.FromFile(@"C:\cg\asd.jpg"); // nesnemize tıklanmadan önceki resmini koyuyoruz
                    dugme.SizeMode = PictureBoxSizeMode.StretchImage;
                    dugme.Name = oyunAlanı2x5[i, j]; //ismini yukarıda oluşturduğumuz dizide aynı alanda bulunan değer yapıyoruz

                    dugme.Click += new EventHandler(click); //nesneye tıklanma event i ekliyoruz kart eşleştirmelerini kontrol için

                    panel1.Controls.Add(dugme); //nesneyi oyun alanına ekliyoruz.

                }

            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            timer3.Stop();
            panel1.Controls.Clear();




            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {

                    PictureBox dugme = new PictureBox();

                    dugme.Top = (235 * i) + 50;
                    dugme.Left = 200 * j + 20;
                    dugme.Width = 150;
                    dugme.Height = 200;


                    dugme.Image = Image.FromFile(@"C:\cg\asd.jpg");
                    dugme.SizeMode = PictureBoxSizeMode.StretchImage;
                    dugme.Name = oyunAlanı3x6[i, j];

                    dugme.Click += new EventHandler(click);

                    panel1.Controls.Add(dugme);

                }

            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            timer3.Stop();
            panel1.Controls.Clear();



            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    PictureBox dugme = new PictureBox();

                    dugme.Top = (195 * i) + 10;
                    dugme.Left = 170 * j + 20;
                    dugme.Width = 120;
                    dugme.Height = 170;


                    dugme.Image = Image.FromFile(@"C:\cg\asd.jpg");
                    dugme.SizeMode = PictureBoxSizeMode.StretchImage;
                    dugme.Name = oyunAlanı4x7[i, j];

                    dugme.Click += new EventHandler(click);

                    panel1.Controls.Add(dugme);

                }

            }
        }
    }
}
