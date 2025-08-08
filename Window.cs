using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Principal;

public class Window : Form
{
    private List<Pixel> pixels;
    private MaskedTextBox numInput;
    private float xScale = 30;
    private float yScale = 0.5f;
    private float YOffset = 0;
    private float XOffset = 0;

    public Window()
    {
        this.Size = new Size(400, 300); // Set form size
        this.Paint += new PaintEventHandler(MyForm_Paint); // Attach Paint event
        this.pixels = new List<Pixel>();
        Button btn = new Button { Text = "Generate", Location = new System.Drawing.Point(10, 10), Size = new Size(150, 20) };
        btn.Click += generate;
        this.Controls.Add(btn);

        numInput = new MaskedTextBox { Location = new System.Drawing.Point(10, 40) };
        numInput.Text = "0001";
        numInput.Mask = "0000";
        Controls.Add(numInput);

        this.Size = new Size(1000, 600);
        this.FormBorderStyle = FormBorderStyle.FixedSingle; // Fixed border, non-resizable
        this.MaximizeBox = false; // Disable maximize button
        // Optional: Disable minimize button if needed
        this.MinimizeBox = false; // Uncomment to disable minimize button
                                  // Optional: Set a specific size
        this.KeyPreview = true;
        this.KeyDown += MovementListener;
        this.MouseWheel += Zoom;
    }
    private void Zoom(object? sender, System.Windows.Forms.MouseEventArgs e)
    {
        int movement = e.Delta / 120;
        xScale *= (1 + movement * 0.1f);
        yScale *= (1 + movement * 0.1f);
        generate(sender, e);
        Invalidate();
    }
    private void MovementListener(object? sender, System.Windows.Forms.KeyEventArgs e)
    {
        if (e.KeyCode == Keys.G || e.KeyCode == Keys.Enter)
        {
            generate(sender, e);
        }
        if (e.KeyCode == Keys.W)
        {
            YOffset += 100;
            generate(sender, e);
        }
        if (e.KeyCode == Keys.S)
        {
            YOffset -= 100;
            generate(sender, e);
        }
        if (e.KeyCode == Keys.A)
        {
            XOffset += 100;
            generate(sender, e);
        }
        if (e.KeyCode == Keys.D)
        {
            XOffset -= 100;
            generate(sender, e);
        }
    }
    private void generate(object? sender, System.EventArgs e)
    {
        if (int.TryParse(numInput.Text, out int n))
        {
            Clear();
            drawLine(-500, 0, 500, 0, 1);
            drawLine(0, -500, 0, 500, 1);
            StagedRocket rocket = new StagedRocket();
            RocketStage flea = new RocketStage(2940, 80000, 7.5f);
            rocket.AddStage(flea, 0);
            rocket.AddStage(flea, 30);
            sketchFunction(rocket.positionFunction);
        }
    }

    private void MyForm_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // Create a pen for outlines and a brush for filling
        using (Pen pen = new Pen(Color.Black, 2))
        using (SolidBrush brush = new SolidBrush(Color.Red))
        {
            /*
            // Draw a line
            g.DrawLine(pen, 50, 50, 200, 50);

            // Draw a rectangle
            g.DrawRectangle(pen, 50, 70, 150, 100);

            // Fill a circle
            g.FillEllipse(brush, 50, 180, 50, 50);
            */
            foreach (Pixel p in pixels)
            {
                //Console.WriteLine("P");
                g.FillRectangle(brush, p.x, p.y, 1, 1);
            }
        }
    }
    [STAThread]
    static void Main()
    {
        Application.Run(new Window());
    }
    public void AddPixel(Pixel p)
    {
        pixels.Add(p);
        this.Invalidate();
    }
    private Pixel Plot(float X, float Y)
    {
        return new Pixel((int)(500 + X * xScale + XOffset), (int)(300 - (Y * yScale) + YOffset));
    }
    private void Clear()
    {
        pixels.Clear();
    }
    private List<Pixel> makeLine(double x1, double y1, double x2, double y2, int thickness)
    {
        List<Pixel> line = new List<Pixel>();

        if (x1 == x2)
        {
            for (float y = (int)Math.Min(y1, y2); y < Math.Max(y1, y2); y += (float)Math.Abs(y1 - y2) / 1000)
            {
                line.Add(Plot((float)x1, y));

            }
        }
        else
        {
            double slope = (y1 - y2) / (x1 - x2);
            for (double d = 0; d < 1; d += 1 / (Math.Abs((x2 - x1)) * xScale * 2))
            {
                line.Add(Plot((float)(x1 + (x2 - x1) * d), (float)(y1 + d * slope * (y2 - y1))));
            }
        }

        List<Pixel> thickened = [.. line];
        for (int i = 1; i < thickness; i++)
        {
            foreach (Pixel p in line)
            {
                thickened.Add(new Pixel(p.x + i, p.y));
                thickened.Add(new Pixel(p.x - i, p.y));
                thickened.Add(new Pixel(p.x, p.y + i));
                thickened.Add(new Pixel(p.x, p.y - i));
            }
        }
        return thickened;
    }
    private void drawLine(double x1, double y1, double x2, double y2, int thickness)
    {
        foreach (Pixel p in makeLine(x1, y1, x2, y2, thickness))
        {
            AddPixel(p);
        }
    }
    private void sketchFunction(Function f)
    {
        for (double i = -200; i < 200; i += 0.01)
        {
            Pixel p = Plot((float)i, f.Evaluate((float)i));
            AddPixel(p);
        }
    }
}