using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace WindowsFormsApplication1    ////BLOCO 4////
{
    public partial class Form1 : Form
    {
        int texTelhado;
        int texPorta;
        int texGrama;
        int lateral = 0;
        int texColuna;
        Vector3d dir = new Vector3d(0, -450, 120);        //direção da câmera
        Vector3d pos = new Vector3d(0, -550, 120);     //posição da câmera
        float camera_rotation = 0;                     //rotação no eixo Z

        public void PosTrelicas(float X, float Z, float Y, float X2, float Z2, float Y2) 
        {
            
            GL.Vertex3(X, Z, Y);
            GL.Vertex3(X2, Z2, Y2);

           
        }
        public void Trelicas(object sender, EventArgs e)
        {
            
            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Quads);
            PosTrelicas(0, -160, 0, 0, -160, 240);
            PosTrelicas(5, -160, 240, 5, -160, 0);
            PosTrelicas(0, -140, 0, 0, -140, 240);
            PosTrelicas(5, -140, 240, 5, -140, 0);

            PosTrelicas(440, -160, 0, 440, -160, 240);
            PosTrelicas(445, -160, 240, 445, -160, 0);
            PosTrelicas(440, -140, 0, 440, -140, 240);
            PosTrelicas(445, -140, 240, 445, -140, 0);

            PosTrelicas(600, -160, 0, 600, -160, 240);
            PosTrelicas(605, -160, 240, 605, -160, 0);
            PosTrelicas(600, -140, 0, 600, -140, 240);
            PosTrelicas(605, -140, 240, 605, -140, 0);
            GL.End();

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texColuna);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); GL.Vertex3(0, -160, 240);
            GL.TexCoord2(7, 0); GL.Vertex3(0, -160, 0);
            GL.TexCoord2(7, 1); GL.Vertex3(0, -140, 0);
            GL.TexCoord2(0, 1); GL.Vertex3(0, -140, 240);
            GL.TexCoord2(0, 0); GL.Vertex3(5, -160, 240);
            GL.TexCoord2(7, 0); GL.Vertex3(5, -160, 0);
            GL.TexCoord2(7, 1); GL.Vertex3(5, -140, 0);
            GL.TexCoord2(0, 1); GL.Vertex3(5, -140, 240);

            GL.TexCoord2(0, 0); GL.Vertex3(440, -160, 240);
            GL.TexCoord2(7, 0); GL.Vertex3(440, -160, 0);
            GL.TexCoord2(7, 1); GL.Vertex3(440, -140, 0);
            GL.TexCoord2(0, 1); GL.Vertex3(440, -140, 240);
            GL.TexCoord2(0, 0); GL.Vertex3(445, -160, 240);
            GL.TexCoord2(7, 0); GL.Vertex3(445, -160, 0);
            GL.TexCoord2(7, 1); GL.Vertex3(445, -140, 0);
            GL.TexCoord2(0, 1); GL.Vertex3(445, -140, 240);

            GL.TexCoord2(0, 0); GL.Vertex3(600, -160, 240);
            GL.TexCoord2(7, 0); GL.Vertex3(600, -160, 0);
            GL.TexCoord2(7, 1); GL.Vertex3(600, -140, 0);
            GL.TexCoord2(0, 1); GL.Vertex3(600, -140, 240);
            GL.TexCoord2(0, 0); GL.Vertex3(605, -160, 240);
            GL.TexCoord2(7, 0); GL.Vertex3(605, -160, 0);
            GL.TexCoord2(7, 1); GL.Vertex3(605, -140, 0);
            GL.TexCoord2(0, 1); GL.Vertex3(605, -140, 240);

            GL.End();

            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //limpa os buffers
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity(); //zera a matriz de projeção com a matriz identidade


            //                 Matrix4 lookat = Matrix4.LookAt(lateral, -500.0f, 1.5f,    
            //                                              1.5f, 5.0f, 1.5f,
            //                                           0.0f, 0.0f, 1.0f);
            Matrix4d lookat = Matrix4d.LookAt(pos.X, pos.Y, pos.Z, dir.X, dir.Y, dir.Z, 0, 0, 1);

            //aplica a transformacao na matriz de rotacao
            GL.LoadMatrix(ref lookat);
            //GL.Rotate(camera_rotation, 0, 0, 1);

            GL.Enable(EnableCap.DepthTest);
            Trelicas( sender, e);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0); GL.Vertex3(500, 0, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 500, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 500);
            GL.End();

            GL.Enable(EnableCap.Texture2D); //habilita o uso de texturas
            GL.BindTexture(TextureTarget.Texture2D, texGrama);
            //GRAMADO
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(-150, -50, 0);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(-150, 300, 0);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(550, 300, 0);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(550, -50, 0);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //COMEÇO PRIMEIRO BLOCO(4)
            //INICIO SALA (41)
            //JANELA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(20, 750, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(350, 750, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(350, 750, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(20, 750, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(550, 750, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(880, 750, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(880, 750, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(550, 750, 45);
            GL.End();

            //PORTA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(130, 0, 0);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(40, 0, 0);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(40, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(130, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //PORTA 2
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(950, 0, 0);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(860, 0, 0);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(860, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(950, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //FRENTE (Sala 41)
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(30, 0, 0);
            GL.Vertex3(0, 750, 0);
            GL.Vertex3(0, 750, 200);
            GL.Vertex3(30, 0, 200);
            GL.End();
           
            //PAREDE FUNDO(sala 41)
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(990, 0, 0);
            GL.Vertex3(960, 750, 0);
            GL.Vertex3(960, 750, 200);
            GL.Vertex3(990, 0, 200);
            GL.End();

            //LATERAL ESQUERDA (sala 41)
            GL.Color3(Color.Blue);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(960, 750, 200);
            GL.Vertex3(960, 750, 0.0);
            GL.Vertex3(0.0, 750, 0.0);
            GL.Vertex3(0.0, 750, 200);
            GL.End();

            //LATERAL DIREITA (sala 41)
            GL.Color3(Color.Blue);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(990, 0.0, 200);
            GL.Vertex3(990, 0.0, 0.0);
            GL.Vertex3(30, 0.0, 0.0);
            GL.Vertex3(30, 0.0, 200);
            GL.End();
            //FIM da sala(41)
            


            //COMEÇO sala(42)

            //JANELA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(620, 430, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(795, 430, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(795, 430, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(620, 430, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(855, 430, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(1030, 430, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(1030, 430, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(855, 430, 45);
            GL.End();

            //PORTA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(670, 0, 0);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(600, 0, 0);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(600, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(670, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //PORTA 2
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(1040, 0, 0);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(970, 0, 0);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(970, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(1040, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //FRENTE
            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(1190, 0, 0);
            GL.Vertex3(1160, 750, 0);
            GL.Vertex3(1160, 750, 200);
            GL.Vertex3(1190, 0, 200);
            GL.End();

            //PAREDE FUNDO
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(2150, 0, 0);
            GL.Vertex3(2120, 750, 0);
            GL.Vertex3(2120, 750, 200);
            GL.Vertex3(2150, 0, 200);
            GL.End();

            //LATERAL ESQUERDA
            GL.Color3(Color.Gray);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(2120, 750, 200);
            GL.Vertex3(2120, 750, 0.0);
            GL.Vertex3(1160, 750, 0.0);
            GL.Vertex3(1160, 750, 200);
            GL.End();

            //LATERAL DIREITA
            GL.Color3(Color.Blue);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(2550, 0.0, 200);
            GL.Vertex3(2550, 0.0, 0.0);
            GL.Vertex3(1190, 0.0, 0.0);
            GL.Vertex3(1190, 0.0, 200);
            GL.End();
            //FIM DO sala(42)

            //COMEÇO DO sala(43)

            //JANELA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(1070, 430, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(1245, 430, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(1245, 430, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(1070, 430, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(1305, 430, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(1480, 430, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(1480, 430, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(1305, 430, 45);
            GL.End();

            //PORTA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(1060, 0, 0);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(1130, 0, 0);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(1130, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(1060, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //PORTA 2
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(1430, 0, 0);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(1500, 0, 0);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(1500, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(1430, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //PAREDE FUNDO
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3080, 0, 0);
            GL.Vertex3(3080, 750, 0);
            GL.Vertex3(3080, 750, 200);
            GL.Vertex3(3080, 0, 200);
            GL.End();

            //LATERAL ESQUERDA
            GL.Color3(Color.Gray);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(3080, 750, 200);
            GL.Vertex3(3080, 750, 0.0);
            GL.Vertex3(2120, 750, 0.0);
            GL.Vertex3(2120, 750, 200);
            GL.End();

            //LATERAL DIREITA
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(3080, 0.0, 200);
            GL.Vertex3(3080, 0.0, 0.0);
            GL.Vertex3(2550, 0.0, 0.0);
            GL.Vertex3(2550, 0.0, 200);
            GL.End();
            //FIM sala (43)

            //COMEÇO sala (44)

            //JANELA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(1905, 430, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(2080, 430, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(2080, 430, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(1905, 430, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(1670, 430, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(1850, 430, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(1850, 430, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(1670, 430, 45);
            GL.End();

            //PORTA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(1650, 0, 0);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(1720, 0, 0);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(1720, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(1650, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //PORTA 2
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(2030, 0, 0);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(2100, 0, 0);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(2100, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(2030, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //FRENTE
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3280, 0, 0);
            GL.Vertex3(3280, 750, 0);
            GL.Vertex3(3280, 750, 200);
            GL.Vertex3(3280, 0, 200);
            GL.End();

            //PAREDE FUNDO
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(4270, 0, 0);
            GL.Vertex3(4240, 750, 0);
            GL.Vertex3(4240, 750, 200);
            GL.Vertex3(4270, 0, 200);
            GL.End();

            //LATERAL ESQUERDA
            GL.Color3(Color.Gray);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(4240, 750, 200);
            GL.Vertex3(4240, 750, 0.0);
            GL.Vertex3(3280, 750, 0.0);
            GL.Vertex3(3280, 750, 200);
            GL.End();

            //LATERAL DIREITA
            GL.Color3(Color.Blue);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(4270, 0.0, 200);
            GL.Vertex3(4270, 0.0, 0.0);
            GL.Vertex3(3280, 0.0, 0.0);
            GL.Vertex3(3280, 0.0, 200);
            GL.End();

            //FIM sala (44)

            //COMEÇO sala(45)

            //JANELA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(2706, 630, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(2942, 630, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(2942, 630, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(2706, 630, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(2470, 630, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(2686, 630, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(2686, 630, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(2470, 630, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(2962, 630, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(3180, 630, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(3180, 630, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(2962, 630, 45);
            GL.End();

            //JANELA BLOCO 5
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(2450, 305, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(2450, 20, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(2450, 20, 60);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(2450, 305, 60);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(2450, 610, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(2450, 325, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(2450, 325, 60);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(2450, 610, 60);
            GL.End();

            //PORTA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(2950, 0, -50);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(3150, 0, -50);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(3150, 0, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(2950, 0, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //FRENTE 
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(4670, 0, 0);
            GL.Vertex3(4640, 1000, 0);
            GL.Vertex3(4640, 1000, 200);
            GL.Vertex3(4670, 0, 200);
            GL.End();
         
            //LATERAL ESQUERDA
            GL.Color3(Color.PapayaWhip);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(6895, 1000, 200);
            GL.Vertex3(6895, 1000, -50.0);
            GL.Vertex3(4640, 1000, -20.0);
            GL.Vertex3(4640, 1000, 200);
            GL.End();

            //LATERAL DIREITA
            GL.Color3(Color.Green);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(6025, 0.0, 200);
            GL.Vertex3(6025, 0.0, -50.0);
            GL.Vertex3(4670, 0.0, -20.0);
            GL.Vertex3(4670, 0.0, 200);
            GL.End();

            //FIM Da SALA(45)

            //COMEÇO SALA(46)

            //JANELA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(3620, 610, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(3620, 344, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(3620, 344, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(3620, 610, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(3620, 324, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(3620, 58, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(3620, 58, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(3620, 324, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(3620, 38, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(3620, -230, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(3620, -230, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(3620, 38, 45);
            GL.End();

            //JANELA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(3220, 630, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(3400, 630, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(3400, 630, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(3220, 630, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(3420, 630, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(3600, 630, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(3600, 630, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(3420, 630, 45);
            GL.End();

            //PORTA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1

            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(162f / 512f, 499f / 512f);
            GL.Vertex3(3200, -25, -50);
            GL.TexCoord2(42f / 512f, 499f / 512f);
            GL.Vertex3(3200, -225, -50);
            GL.TexCoord2(42f / 512f, 195f / 512f);
            GL.Vertex3(3200, -225, 180);
            GL.TexCoord2(162f / 512f, 195f / 512f);
            GL.Vertex3(3200, -25, 180);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

          
            //PAREDE FUNDO
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(6925, -265, 0);
            GL.Vertex3(6895, 1030, 0);
            GL.Vertex3(6895, 1030, 200);
            GL.Vertex3(6925, -265, 200);
            GL.End();

            //PAREDE FRENTE
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(6025, 1000, -50);
            GL.Vertex3(6025, -265, -50);
            GL.Vertex3(6025, -265, 200);
            GL.Vertex3(6025, 1000, 200);
            GL.End();

            //LATERAL DIREITA
            GL.Color3(Color.Green);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(6925, -265.0, 200);
            GL.Vertex3(6925, -265.0, -50.0);
            GL.Vertex3(6025, -265.0, -50.0);
            GL.Vertex3(6025, -265.0, 200);
            GL.End();
            //FIM sala 46

            GL.Enable(EnableCap.Texture2D); //habilita o uso de texturas
            GL.BindTexture(TextureTarget.Texture2D, texGrama);
            //RAMPA
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2750, -250, -50);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2750, 0, -50);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2250, 0, 0);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2250, -250, 0);
            GL.End();

            //TERRA
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2450, 630, -20);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2450, 0, -20);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2250, 0, 0);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2250, 630, 0);
            GL.End();

            //TERRA 2
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2250, 630, 0);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2250, 0, 0);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2100, 0, 0);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2100, 630, 0);
            GL.End();

            //CALÇADA
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2250, -250, 0);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2250, 0, 0);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(0, 0, 0);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(0, -250, 0);
            GL.End();

            //CALÇADA 2
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2750, -250, -50);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2750, 0, -50);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3200, 0, -50);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3200, -250, -50);
            GL.End();

            //CHÃO DA SALA 6
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 220, -50);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, -250, -50);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, -250, -50);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 220, -50);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 220, -50);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, 220, -40);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, 220, -40);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 220, -50);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 320, -40);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, 220, -40);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, 220, -40);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 320, -40);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 320, -40);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, 320, -30);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, 320, -30);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 320, -40);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 420, -30);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, 320, -30);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, 320, -30);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 420, -30);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 420, -30);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, 420, -20);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, 420, -20);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 420, -30);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 520, -20);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, 420, -20);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, 420, -20);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 520, -20);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 520, -20);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, 520, -10);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, 520, -10);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 520, -20);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(3200, 630, -10);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(3200, 520, -10);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3620, 520, -10);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3620, 630, -10);
            GL.End();

            //CHÃO DA SALA 5
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2950, 630, -50);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2950, 0, -50);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(3200, 0, -50);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(3200, 630, -50);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2850, 630, -40);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2850, 0, -40);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2950, 0, -40);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2950, 630, -40);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2950, 630, -40);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2950, 0, -40);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2950, 0, -50);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2950, 630, -50);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2850, 630, -30);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2850, 0, -30);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2850, 0, -40);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2850, 630, -40);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2750, 630, -30);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2750, 0, -30);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2850, 0, -30);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2850, 630, -30);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2750, 630, -20);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2750, 0, -20);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2750, 0, -30);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2750, 630, -30);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2650, 630, -20);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2650, 0, -20);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2750, 0, -20);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2750, 630, -20);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2650, 630, -10);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2650, 0, -10);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2650, 0, -20);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2650, 630, -20);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2550, 630, -10);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2550, 0, -10);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2650, 0, -10);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2650, 630, -10);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2550, 630, 0);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2550, 0, 0);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2550, 0, -10);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2550, 630, -10);

            GL.TexCoord2(0.0f, 5.0f); GL.Vertex3(2450, 630, 0);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(2450, 0, 0);
            GL.TexCoord2(8.0f, 0.0f); GL.Vertex3(2550, 0, 0);
            GL.TexCoord2(8.0f, 5.0f); GL.Vertex3(2550, 630, 0);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            //FIM BLOCO 4//

            //BLOCO 5//

            //COMEÇO PRIMEIRO BLOCO
            /*
            //JANELA
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texPorta);
            GL.Color3(Color.Transparent);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(20, 1430, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(185, 1430, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(185, 1430, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(20, 1430, 45);

            GL.TexCoord2(232f / 512f, 15f / 512f); GL.Vertex3(245, 1430, 180);
            GL.TexCoord2(505f / 512f, 15f / 512f); GL.Vertex3(430, 1430, 180);
            GL.TexCoord2(505f / 512f, 175f / 512f); GL.Vertex3(430, 1430, 45);
            GL.TexCoord2(232f / 512f, 175f / 512f); GL.Vertex3(245, 1430, 45);
            GL.End();
            */
            ////PORTA
            //GL.Enable(EnableCap.Texture2D);
            //GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            //GL.Color3(Color.Transparent);
            //GL.Begin(PrimitiveType.Quads);
            //GL.TexCoord2(162f / 512f, 499f / 512f);
            //GL.Vertex3(490, 0, 0);
            //GL.TexCoord2(42f / 512f, 499f / 512f);
            //GL.Vertex3(400, 0, 0);
            //GL.TexCoord2(42f / 512f, 195f / 512f);
            //GL.Vertex3(400, 0, 180);
            //GL.TexCoord2(162f / 512f, 195f / 512f);
            //GL.Vertex3(490, 0, 180);
            //GL.End();
            //GL.Disable(EnableCap.Texture2D);

            ////PORTA 2
            //GL.Enable(EnableCap.Texture2D);
            //GL.BindTexture(TextureTarget.Texture2D, texPorta); //utiliza a textura1
            //GL.Color3(Color.Transparent);
            //GL.Begin(PrimitiveType.Quads);
            //GL.TexCoord2(162f / 512f, 499f / 512f);
            //GL.Vertex3(850, 0, 0);
            //GL.TexCoord2(42f / 512f, 499f / 512f);
            //GL.Vertex3(760, 0, 0);
            //GL.TexCoord2(42f / 512f, 195f / 512f);
            //GL.Vertex3(760, 0, 180);
            //GL.TexCoord2(162f / 512f, 195f / 512f);
            //GL.Vertex3(850, 0, 180);
            //GL.End();


            GL.Disable(EnableCap.Texture2D);
            // INicio SALA DIRETOR IMESA, Secretaria Imesa, Coordenadoria de cursos, Supervisão Academico
            //FRENTE
            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(0, 1750, 0);
            GL.Vertex3(0, 2725, 0);
            GL.Vertex3(0, 2725, 200);
            GL.Vertex3(0, 1750, 200);
            GL.End();

            //PAREDE FUNDO
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(888, 2325, 0);
            GL.Vertex3(888, 2725, 0);
            GL.Vertex3(888, 2725, 200);
            GL.Vertex3(888, 2325, 200);
            GL.End();
            
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(888, 1750, 0);
            GL.Vertex3(888, 2155, 0);
            GL.Vertex3(888, 2155, 200);
            GL.Vertex3(888, 1750, 200);
            GL.End();

            //LATERAL ESQUERDA
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(888, 2725, 200);
            GL.Vertex3(888, 2725, 0.0);
            GL.Vertex3(0.0, 2725, 0.0);
            GL.Vertex3(0.0, 2725, 200);
            
            //LATERAL DIREITA
            GL.Color3(Color.Blue);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(888, 1750, 200);
            GL.Vertex3(888, 1750, 0);
            GL.Vertex3(0.0, 1750, 0);
            GL.Vertex3(0.0, 1750, 200);

            //meio sala coordenaria e diretor
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(385, 2240, 200);
            GL.Vertex3(385, 2240, 0.0);
            GL.Vertex3(0.0, 2240, 0.0);
            GL.Vertex3(0.0, 2240, 200);

            //circulação esquerda
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(778, 2325, 200);
            GL.Vertex3(778, 2325, 0.0);
            GL.Vertex3(428, 2325, 0.0);
            GL.Vertex3(428, 2325, 200);

            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(868, 2325, 200);
            GL.Vertex3(868, 2325, 180);
            GL.Vertex3(778, 2325, 180);
            GL.Vertex3(778, 2325, 200);

            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(888, 2325, 200);
            GL.Vertex3(888, 2325, 0.0);
            GL.Vertex3(868, 2325, 0.0);
            GL.Vertex3(868, 2325, 200);

            //circulação direita
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(888, 2155, 200);
            GL.Vertex3(888, 2155, 0.0);
            GL.Vertex3(428, 2160, 0.0);
            GL.Vertex3(428, 2160, 200);

            //meio sala diretor secretaria
            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(447, 2325, 0);
            GL.Vertex3(447, 2725, 0);
            GL.Vertex3(447, 2725, 200);
            GL.Vertex3(447, 2325, 200);
            GL.End();

            //meio sala coordernaria e supervisao
            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(625, 1750, 0);
            GL.Vertex3(625, 2160, 0);
            GL.Vertex3(625, 2160, 200);
            GL.Vertex3(625, 1750, 200);
            GL.End();

            // final SALA DIRETOR IMESA, Secretaria Imesa, Coordenadoria de cursos, Supervisão A
            //INICIO SALA DE ESPERA
            //LATERAL ESQUERDA
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(1103, 2725, 200);
            GL.Vertex3(1103, 2725, 0.0);
            GL.Vertex3(888, 2725, 0.0);
            GL.Vertex3(888, 2725, 200);

            //LATERAL direita
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(1000, 2080, 200);
            GL.Vertex3(1000, 2080, 0.0);
            GL.Vertex3(888, 2080, 0.0);
            GL.Vertex3(888, 2080, 200);

            //parede do fundo
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(1103, 1985, 0);
            GL.Vertex3(1103, 2725, 0);
            GL.Vertex3(1103, 2725, 200);
            GL.Vertex3(1103, 1985, 200);
            GL.End();

            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(1000, 1985, 0);
            GL.Vertex3(1000, 2080, 0);
            GL.Vertex3(1000, 2080, 200);
            GL.Vertex3(1000, 1985, 200);
            GL.End();

            //FIM SALA DE ESPERA
            //inicio SALA DE compras, rh e contabilidade
            //LATERAL ESQUERDA
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(2886, 1985, 200);
            GL.Vertex3(2886, 1985, 0.0);
            GL.Vertex3(1103, 1985, 0.0);
            GL.Vertex3(1103, 1985, 200);

            //LATERAL direita
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(2886, 2725, 200);
            GL.Vertex3(2886, 2725, 0.0);
            GL.Vertex3(1103, 2725, 0.0);
            GL.Vertex3(1103, 2725, 200);

            //frente compras
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(1545, 2130, 200);
            GL.Vertex3(1545, 2130, 0.0);
            GL.Vertex3(1103, 2130, 0.0);
            GL.Vertex3(1103, 2130, 200);

            //parede meio compras rh
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(1545, 1985, 0);
            GL.Vertex3(1545, 2725, 0);
            GL.Vertex3(1545, 2725, 200);
            GL.Vertex3(1545, 1985, 200);
            GL.End();

            //Meio rh/rh
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(2035, 1985, 0);
            GL.Vertex3(2035, 2725, 0);
            GL.Vertex3(2035, 2725, 200);
            GL.Vertex3(2035, 1985, 200);
            GL.End();

            //Meio rh/contabilidade
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(2453, 1985, 0);
            GL.Vertex3(2453, 2725, 0);
            GL.Vertex3(2453, 2725, 200);
            GL.Vertex3(2453, 1985, 200);
            GL.End();

            //circulação
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(2886, 2125, 200);
            GL.Vertex3(2886, 2125, 0.0);
            GL.Vertex3(2453, 2125, 0.0);
            GL.Vertex3(2453, 2125, 200);


            //fundo
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(2886, 1985, 0);
            GL.Vertex3(2886, 2725, 0);
            GL.Vertex3(2886, 2725, 200);
            GL.Vertex3(2886, 1985, 200);
            GL.End();

            //fim SALA DE compras, rh e contabilidade
            //inicio telefonista, sanitario, diretoria executiva imesa e assessoria juridica
            //frente
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3101, 1770, 0);
            GL.Vertex3(3101, 2725, 0);
            GL.Vertex3(3101, 2725, 200);
            GL.Vertex3(3101, 1770, 200);
            GL.End();

            //parede tesouraria/circulação
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(3501, 2110, 200);
            GL.Vertex3(3501, 2110, 0.0);
            GL.Vertex3(3101, 2110, 0.0);
            GL.Vertex3(3101, 2110, 200);
            GL.End();

            //imenda das paredes
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3501, 2110, 0);
            GL.Vertex3(3501, 2155, 0);
            GL.Vertex3(3501, 2155, 200);
            GL.Vertex3(3501, 2110, 200);
            GL.End();

            //parede tesouraria/circulação
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(3941, 2155, 200);
            GL.Vertex3(3941, 2155, 0.0);
            GL.Vertex3(3501, 2155, 0.0);
            GL.Vertex3(3501, 2155, 200);
            GL.End();

            //tesouraria/ sec. mantenedora
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3941, 1770, 0);
            GL.Vertex3(3941, 2155, 0);
            GL.Vertex3(3941, 2155, 200);
            GL.Vertex3(3941, 1770, 200);
            GL.End();

           // coluna porta sec. mantenedora
            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3941, 2155, 0);
            GL.Vertex3(3961, 2170, 0);
            GL.Vertex3(3961, 2170, 200);
            GL.Vertex3(3941, 2155, 200);
            GL.End();

            // coluna porta ass
            GL.Color3(Color.Yellow);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3931, 2325, 0);
            GL.Vertex3(3911, 2340, 0);
            GL.Vertex3(3911, 2340, 200);
            GL.Vertex3(3931, 2325, 200);
            GL.End();

            //sala telefonista
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3351, 2385, 0);
            GL.Vertex3(3351, 2725, 0);
            GL.Vertex3(3351, 2725, 200);
            GL.Vertex3(3351, 2385, 200);
            GL.End();

            //sala banheiro
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3471, 2340, 0);
            GL.Vertex3(3471, 2725, 0);
            GL.Vertex3(3471, 2725, 200);
            GL.Vertex3(3471, 2340, 200);
            GL.End();

            //sala diretoria
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(3911, 2340, 0);
            GL.Vertex3(3911, 2725, 0);
            GL.Vertex3(3911, 2725, 200);
            GL.Vertex3(3911, 2340, 200);
            GL.End();

            //frente banheiro/ tesolraria
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(3471, 2385, 200);
            GL.Vertex3(3471, 2385, 0.0);
            GL.Vertex3(3101, 2385, 0.0);
            GL.Vertex3(3101, 2385, 200);
            GL.End();

            //frente diretoria
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(3911, 2340, 200);
            GL.Vertex3(3911, 2340, 0.0);
            GL.Vertex3(3471, 2340, 0.0);
            GL.Vertex3(3471, 2340, 200);
            GL.End();

            //meio acessoria, mantenedora
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(4411, 2257, 200);
            GL.Vertex3(4411, 2257, 0.0);
            GL.Vertex3(4001, 2257, 0.0);
            GL.Vertex3(4001, 2257, 200);
            GL.End();



            //LATERAL ESQUERDA
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(4411, 2725, 200);
            GL.Vertex3(4411, 2725, 0.0);
            GL.Vertex3(3101, 2725, 0.0);
            GL.Vertex3(3101, 2725, 200);
            GL.End();

            //LATERAL direita
            GL.Color3(Color.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(4411, 1770, 200);
            GL.Vertex3(4411, 1770, 0.0);
            GL.Vertex3(3101, 1770, 0.0);
            GL.Vertex3(3101, 1770, 200);
            GL.End();

            //fundo
            GL.Color3(Color.PaleVioletRed);
            GL.Begin(PrimitiveType.Polygon); //escolhe o tipo da primitiva
            GL.Vertex3(4411, 1770, 0);
            GL.Vertex3(4411, 2725, 0);
            GL.Vertex3(4411, 2725, 200);
            GL.Vertex3(4411, 1770, 200);
            GL.End();

            //EXEMPLO DE OBJETO TRANSPARENTE
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Color4(0.1f, 0.5f, 0.6f, 0.6f); //Último parâmetro é a porcentagem de trasparência

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex3(-80, 50, 0);
            GL.Vertex3(-80, 100, 0);
            GL.Vertex3(-80, 100, 50);
            GL.Vertex3(-80, 50, 50);
            GL.End();
            GL.Disable(EnableCap.Blend);

            glControl1.SwapBuffers(); //troca os buffers de frente e de fundo 

        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.Black);         // definindo a cor de limpeza do fundo da tela
            GL.Enable(EnableCap.Light0);

            texTelhado = LoadTexture("../../textura/telhado.jpg");
            texPorta = LoadTexture("../../textura/portajanela.jpg");
            texGrama = LoadTexture("../../textura/grama.jpg");
            texColuna = LoadTexture("../../textura/coluna.png");
            SetupViewport();                      //configura a janela de pintura
        }

        private void SetupViewport() //configura a janela de projeção 
        {
            int w = glControl1.Width; //largura da janela
            int h = glControl1.Height; //altura da janela

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(1f, w / (float)h, 0.1f, 2000.0f);
            GL.LoadIdentity(); //zera a matriz de projeção com a matriz identidade

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Viewport(0, 0, w, h); // usa toda area de pintura da glcontrol
            lateral = w / 2;

        }

        static int LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id;//= GL.GenTexture(); 

            GL.GenTextures(1, out id);
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(filename);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return id;
        }
        private void calcula_direcao()
        {
            dir.X = pos.X + (Math.Sin(camera_rotation * Math.PI / 180) * 1000);
            dir.Y = pos.Y + (Math.Cos(camera_rotation * Math.PI / 180) * 1000);
        }
        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > lateral)
            {
                camera_rotation += 2;
            }
            if (e.X < lateral)
            {
                camera_rotation -= 2;
            }
            lateral = e.X;
            calcula_direcao();
            glControl1.Invalidate();
        }

        private void glControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            float a = camera_rotation;
            int tipoTecla = 0;
            if (e.KeyCode == Keys.Left)
            {
                a -= 90;
                tipoTecla = 1;
            }
            if (e.KeyCode == Keys.Right)
            {
                a += 90;
                tipoTecla = 1;
            }
            if (e.KeyCode == Keys.Up)
            { tipoTecla = 1; }
            if (e.KeyCode == Keys.Down)
            {
                a += 180;
                tipoTecla = 1;
            }

            if (e.KeyCode == Keys.D)
            {
                a += 1;
                tipoTecla = 2;
            }
            if (e.KeyCode == Keys.A)
            {
                a -= 1;
                tipoTecla = 2;
            }
            if (tipoTecla == 1)
            {
                if (a < 0) a += 360;
                if (a > 360) a -= 360;
                label2.Text = a.ToString();
                pos.X += (Math.Sin(a * Math.PI / 180) * 10);
                pos.Y += (Math.Cos(a * Math.PI / 180) * 10);
                calcula_direcao();
                glControl1.Invalidate();
            }

            if (tipoTecla == 2)
            {
                camera_rotation = a;
                calcula_direcao();
                glControl1.Invalidate();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            glControl1.Width = Form1.ActiveForm.Width - 10;
            glControl1.Height = Form1.ActiveForm.Height - 10;
            SetupViewport();
            glControl1.Invalidate();
        }

    }
}




/*
        GL.Color3(Color.Yellow);
        //LATERAL DIREITA
        GL.Begin(PrimitiveType.Quads);
        GL.Vertex3(350, 0.0, 200);
        GL.Vertex3(350, 0.0, 0.0);
        GL.Vertex3(250, 0.0, 0.0);
        GL.Vertex3(250, 0.0, 200);

        GL.Vertex3(100, 0.0, 60.0);
        GL.Vertex3(100, 0.0, 0.0);
        GL.Vertex3(250, 0.0, 0.0);
        GL.Vertex3(250, 0.0, 60.0);
        GL.Vertex3(100, 0.0, 200.0);
        GL.Vertex3(100, 0.0, 40.0);
        GL.Vertex3(250, 0.0, 40.0);
        GL.Vertex3(250, 0.0, 200.0);

        GL.Vertex3(0.0, 0.0, 0.0);
        GL.Vertex3(0.0, 0.0, 200);
        GL.Vertex3(100.0, 0.0, 200.0);
        GL.Vertex3(100.0, 0.0, 0.0);

        GL.End();

/*
        //TELHADO DIREITA
        GL.Enable(EnableCap.Texture2D);
        GL.BindTexture(TextureTarget.Texture2D, texTelhado);
        GL.Color3(Color.White);
        GL.Begin(PrimitiveType.Quads);
        GL.TexCoord2(0.0f, 3.0f);
        GL.Vertex3(-40, -40, 184);
        GL.TexCoord2(0.0f, 0.0f);
        GL.Vertex3(-40, 125, 250);
        GL.TexCoord2(2.0f, 0.0f);
        GL.Vertex3(390, 125, 250);
        GL.TexCoord2(2.0f, 3.0f);
        GL.Vertex3(390, -40, 184);
        GL.End();

        //TELHADO ESQUERDA
        GL.Color3(Color.Transparent);
        GL.Begin(PrimitiveType.Quads);
        GL.TexCoord2(0.0f, 3.0f);
        GL.Vertex3(-40, 290, 184);
        GL.TexCoord2(0.0f, 0.0f);
        GL.Vertex3(-40, 125, 250);
        GL.TexCoord2(2.0f, 0.0f);
        GL.Vertex3(390, 125, 250);
        GL.TexCoord2(2.0f, 3.0f);
        GL.Vertex3(390, 290, 184);
        GL.End();
        GL.Disable(EnableCap.Texture2D);
        //BEIRAL
        GL.Color3(Color.SaddleBrown);
        GL.Begin(PrimitiveType.Quads);
        GL.Vertex3(-40, -40, 174);
        GL.Vertex3(-40, 125, 240);
        GL.Vertex3(-40, 125, 250);
        GL.Vertex3(-40, -40, 184);

        GL.Vertex3(-40, 290, 174);
        GL.Vertex3(-40, 290, 184);
        GL.Vertex3(-40, 125, 250);
        GL.Vertex3(-40, 125, 240);

        GL.Vertex3(390, -40, 174);
        GL.Vertex3(390, 125, 240);
        GL.Vertex3(390, 125, 250);
        GL.Vertex3(390, -40, 184);

        GL.Vertex3(390, 290, 174);
        GL.Vertex3(390, 290, 184);
        GL.Vertex3(390, 125, 250);
        GL.Vertex3(390, 125, 240);

        GL.Vertex3(390, -40, 174);
        GL.Vertex3(390, -40, 184);
        GL.Vertex3(-40, -40, 184);
        GL.Vertex3(-40, -40, 174);

        GL.Vertex3(390, 290, 174);
        GL.Vertex3(390, 290, 184);
        GL.Vertex3(-40, 290, 184);
        GL.Vertex3(-40, 290, 174);

        GL.End();
        */
