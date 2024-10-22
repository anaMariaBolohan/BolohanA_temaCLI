using System;
using System.Drawing;
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

        // Constructor
        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
        }

        // Eveniment pentru tastatură
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();
            if (e.Key == Key.F11)
                this.WindowState = this.WindowState == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
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

            // Aici se poate schimba între proiecția ortografică și cea în perspectivă
            // Proiecție ortografică:
            // GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0); 

            // Proiecție în perspectivă:
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

            // Randare triunghi
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.MidnightBlue); GL.Vertex2(-1.0f, 1.0f);
            GL.Color3(Color.SpringGreen); GL.Vertex2(0.0f, -1.0f);
            GL.Color3(Color.Ivory); GL.Vertex2(1.0f, 1.0f);
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
