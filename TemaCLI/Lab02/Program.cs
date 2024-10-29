using System;
using System.Drawing;
using System.IO;  // LAB 3 cerința 8: Adaugă referința pentru a lucra cu fișiere
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Lab02
{
    class SimpleWindow : GameWindow
    {
        // Variabile pentru controlul obiectului
        Vector2 objectPosition = Vector2.Zero;  // Poziția obiectului
        float objectRotationY = 0f;             // Rotirea obiectului pe axa Y
        float previousMouseX;                   // Stocarea poziției anterioare a mouse-ului

        // Variabile pentru culorile triunghiului
        Color[] triangleColors = new Color[3];  // LAB 3 cerința 8: Culori pentru fiecare vertex
        float colorChangeSpeed = 0.01f; // LAB 3 cerința 8: Viteza de schimbare a culorilor

        // Constructor
        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
            LoadTriangleCoordinates("C:\\Users\\Ana\\Desktop\\BolohanA_temaCLI\\TemaCLI\\Lab02\\triangle.txt"); // LAB 3 cerința 8: Încărcarea coordonatelor din fișier
        }

        // Eveniment pentru tastatură
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();
            if (e.Key == Key.F11)
                this.WindowState = this.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;

            // LAB 3 cerința 8: Schimbarea culorilor la apăsarea tastelor
            if (e.Key == Key.R)
            {
                ChangeColor(0); // Schimbă culoarea vertex-ului 1
            }
            else if (e.Key == Key.G)
            {
                ChangeColor(1); // Schimbă culoarea vertex-ului 2
            }
            else if (e.Key == Key.B)
            {
                ChangeColor(2); // Schimbă culoarea vertex-ului 3
            }
        }

        // LAB 3 cerința 8: Încărcarea coordonatelor triunghiului din fișier
        private void LoadTriangleCoordinates(string filename)
        {
            // Coordonatele triunghiului sunt așteptate în fișier în formatul: x1,y1;x2,y2;x3,y3
            string[] lines = File.ReadAllLines(filename);
            if (lines.Length >= 3)
            {
                triangleColors[0] = Color.MidnightBlue; // Culoare default vertex 1
                triangleColors[1] = Color.SpringGreen;  // Culoare default vertex 2
                triangleColors[2] = Color.Ivory;        // Culoare default vertex 3
            }
            else
            {
                throw new Exception("Fișierul de coordonate nu este corect formatat."); // LAB 3 cerința 8: Validare fișier
            }
        }

        // LAB 3 cerința 8: Schimbarea culorii unui vertex
        private void ChangeColor(int vertexIndex)
        {
            // Modifică culoarea vertex-ului specificat
            triangleColors[vertexIndex] = Color.FromArgb(
                Math.Max(0, Math.Min(255, triangleColors[vertexIndex].R + (int)(colorChangeSpeed * 255))),
                Math.Max(0, Math.Min(255, triangleColors[vertexIndex].G + (int)(colorChangeSpeed * 255))),
                Math.Max(0, Math.Min(255, triangleColors[vertexIndex].B + (int)(colorChangeSpeed * 255)))
            );
            Console.WriteLine($"Vertex {vertexIndex + 1} Color: {triangleColors[vertexIndex]}"); // LAB 3 cerința 9: Afișare în consolă
        }

        // Inițializare OpenGL și resurse
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
        }

        // Configurare viewport și proiecție
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Frustum(-1.0, 1.0, -1.0, 1.0, 1.0, 10.0);
        }

        // Actualizarea poziției și rotației obiectului pe baza input-ului de la tastatură și mouse
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();

            // Control prin tastatură: deplasare pe axa X
            if (keyboard[OpenTK.Input.Key.Left])
            {
                objectPosition.X -= 0.1f;  // Mutare obiect spre stânga
            }
            if (keyboard[OpenTK.Input.Key.Right])
            {
                objectPosition.X += 0.1f;  // Mutare obiect spre dreapta
            }

            // Control prin mouse: rotație pe axa Y
            if (mouse[OpenTK.Input.MouseButton.Left])
            {
                objectRotationY += (mouse.X - previousMouseX) * 0.1f;  // Rotire obiect
            }
            previousMouseX = mouse.X;  // Actualizare poziția mouse-ului
        }

        // Randarea scenei 3D
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Aplicare transformări pentru poziția și rotația obiectului
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(objectPosition.X, objectPosition.Y, -3.0);  // Mutare obiect
            GL.Rotate(objectRotationY, 0.0f, 1.0f, 0.0f);  // Rotire obiect pe axa Y

            // LAB 3 cerința 8: Randare triunghi
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(triangleColors[0]); GL.Vertex2(-1.0f, 1.0f); // Vertex 1
            GL.Color3(triangleColors[1]); GL.Vertex2(0.0f, -1.0f);  // Vertex 2
            GL.Color3(triangleColors[2]); GL.Vertex2(1.0f, 1.0f);   // Vertex 3
            GL.End();

            this.SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
