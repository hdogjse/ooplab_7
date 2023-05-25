using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;

namespace ooplab_4
{
    public partial class Form1 : Form
    {
        private Color _color = Color.Blue;
        class gOBJ
        {


            protected class Model
            {
                public bool PointOnArea(Rectangle area, int x, int y)
                {
                    if (x >= area.X && x <= area.X + area.Width && y >= area.Y && y <= area.Y + area.Height)
                    {
                        return true;
                    }
                    return false;
                }
            }


            protected Rectangle area;

            protected Model model;

            protected Pen pen;


            protected bool selected = false;

            public virtual void SetSelected()
            {
                selected = true;

            }

            public virtual void SetColor(Color color)
            {
                pen.Color = color;
            }

            public virtual void Save(XmlElement element)
            {
                element.SetAttribute("X", area.X.ToString());
                element.SetAttribute("Y", area.Y.ToString());
                element.SetAttribute("Width", area.Width.ToString());
                element.SetAttribute("Height", area.Height.ToString());

                element.SetAttribute("Color", pen.Color.ToArgb().ToString());
            }

            public virtual void Load(XmlNode element)
            {
                area.X = Convert.ToInt32(element.Attributes["X"].Value);
                area.Y = Convert.ToInt32(element.Attributes["Y"].Value);
                area.Width = Convert.ToInt32(element.Attributes["Width"].Value);
                area.Height = Convert.ToInt32(element.Attributes["Height"].Value);
                pen.Color = Color.FromArgb(Convert.ToInt32(element.Attributes["Color"].Value));
            }

            public virtual void SetUnSelected()
            {
                selected = false;
            }

            public int X()
            {
                return area.X;
            }

            public int Y()
            {
                return area.Y;
            }

            public virtual void SetPos(int x, int y)
            {
                area.X = x;
                area.Y = y;
            }

            public virtual void SetWidth(int width)
            {
                area.Width = width;
            }

            public int Width()
            {
                return area.Width;
            }

            public virtual void SetHeight(int height)
            {
                area.Height = height;
            }

            public int Height()
            {
                return area.Height;
            }

            public bool PointOnMe(int x, int y)
            {
                return model.PointOnArea(area, x, y);
            }
            public virtual void Draw(PaintEventArgs ev)
            {
                if (selected)
                {
                    Pen pen = new Pen(Color.Brown);
                    pen.DashStyle = DashStyle.Dash;
                    pen.Width = 5;
                    Rectangle rec;
                    rec = area;
                    rec.X -= 5;
                    rec.Y -= 5;
                    rec.Width += 10;
                    rec.Height += 10;
                    ev.Graphics.DrawRectangle(pen, rec);
                }
            }


        }

        class CCircle : gOBJ
        {

            public CCircle(int x, int y)
            {
                pen = new Pen(Color.Blue, 1);
                model = new Model();
                area.Height = 100;
                area.Width = 100;
                area.X = x;
                area.Y = y;
            }

            public override void Draw(PaintEventArgs ev)
            {
                base.Draw(ev);
                ev.Graphics.DrawEllipse(pen, area);
            }

            public override void Save(XmlElement element)
            {
                element.SetAttribute("Type", "Circle");
                base.Save(element);
            }
        }

        class CRect : gOBJ
        {

            public CRect(int x, int y)
            {
                pen = new Pen(Color.Blue, 1);
                model = new Model();
                area.Height = 100;
                area.Width = 100;
                area.X = x;
                area.Y = y;
            }

            public override void Draw(PaintEventArgs ev)
            {
                base.Draw(ev);
                ev.Graphics.DrawRectangle(pen, area);
            }

            public override void Save(XmlElement element)
            {
                element.SetAttribute("Type", "Rect");
                base.Save(element);
            }
        }


        class CLine : gOBJ
        {
            private Brush brush;
            public CLine(int x, int y)
            {
                brush = new SolidBrush(Color.Blue);
                pen = new Pen(Color.Blue, 1);
                model = new Model();
                area.Height = 10;
                area.Width = 100;
                area.X = x;
                area.Y = y;
            }

            public override void SetColor(Color color)
            {
                base.SetColor(color);
                brush = new SolidBrush(color);
            }

            public override void Draw(PaintEventArgs ev)
            {
                base.Draw(ev);
                ev.Graphics.FillRectangle(brush, area);
            }
            public override void Save(XmlElement element)
            {
                element.SetAttribute("Type", "Line");
                base.Save(element);
            }

            public override void Load(XmlNode element)
            {
                base.Load(element);

                brush = new SolidBrush(pen.Color);
            }
        }
        class CGroup : gOBJ
        {
            List<gOBJ> childs;

            public CGroup()
            {
                model = new Model();
                childs = new List<gOBJ>();
                area.X = 0;
                area.Y = 0;
                area.Width = 0;
                area.Height = 0;
                pen = new Pen(Color.Blue);
            }
                public CGroup(List<gOBJ> objects)
            {
                model = new Model();
                childs = new List<gOBJ>(objects);
                area.X = childs[0].X();
                area.Y = childs[0].Y();
                area.Width = childs[0].Width();
                area.Height = childs[0].Height();
                pen = new Pen(Color.Blue);
                for (int i = 1; i < childs.Count; i++)
                {
                    if (area.X > childs[i].X())
                        area.X = childs[i].X();
                    if (area.X + area.Width < childs[i].X() + childs[i].Width())
                    {
                        area.Width = -area.X + childs[i].X() + childs[i].Width();
                    }
                    if (area.Y > childs[i].Y())
                        area.Y = childs[i].Y();
                    if (area.Y + area.Height < childs[i].Y() + childs[i].Height())
                        area.Height = -area.Y + childs[i].Y() + childs[i].Height();

                }
            }

            public override void SetHeight(int height)
            {
                int diff = area.Height - height;
                for (int i = 0; i < childs.Count; i++)
                {
                    childs[i].SetHeight(childs[i].Height() - diff);
                }
                base.SetHeight(height);

            }

            public override void SetWidth(int width)
            {
                int diff = area.Width - width;
                for (int i = 0; i < childs.Count; i++)
                {
                    childs[i].SetWidth(childs[i].Width() - diff);
                }
                base.SetWidth(width);
            }

            public override void SetPos(int x, int y)
            {
                int diff_x = area.X - x;
                int diff_y = area.Y - y;
                for (int i = 0; i < childs.Count; i++)
                {
                    childs[i].SetPos(childs[i].X() - diff_x, childs[i].Y() - diff_y);
                }
                base.SetPos(x, y);
            }
            public override void Draw(PaintEventArgs ev)
            {
                base.Draw(ev);
                for (int i = 0; i < childs.Count; i++)
                {
                    childs[i].Draw(ev);
                }
            }
            public override void Save(XmlElement element)
            {
                element.SetAttribute("Type", "Group");
                for (int i = 0; i < childs.Count; i++)
                {
                    XmlElement el = element.OwnerDocument.CreateElement("obj");

                    childs[i].Save(el);
                    element.AppendChild(el);
                }
                base.Save(element);
            }

            public override void Load(XmlNode element)
            {
                gOBJ obj;
                childs.Clear();
                base.Load(element);
                for (int i = 0; i < element.ChildNodes.Count; i++)
                {
                    switch (element.ChildNodes[i].Attributes["Type"].Value)
                    {
                        case "Circle":
                            obj = new CCircle(0, 0);
                            obj.Load(element.ChildNodes[i]);
                            childs.Add(obj);
                            break;
                        case "Rect":
                            obj = new CRect(0, 0);
                            obj.Load(element.ChildNodes[i]);
                            childs.Add(obj);
                            break;
                        case "Group":
                            obj = new CGroup();
                            obj.Load(element.ChildNodes[i]); childs.Add(obj);
                            break;
                        case "Line":
                            obj = new CLine(0,0);
                            obj.Load(element.ChildNodes[i]); childs.Add(obj);
                            break;
                    }
                }
            }
        }
        List<gOBJ> circles;

        List<gOBJ> selected_circles;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            circles = new List<gOBJ>();
            selected_circles = new List<gOBJ>();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                circles[i].Draw(e);
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button != MouseButtons.Left)
            //    return;
            //for (int i = 0; i < circles.Count; i++)
            //{
            //    if (circles[i].PointOnMe(e.X, e.Y))
            //    {
            //        if (!MultiSelect.Checked |
            //            MultiSelect.Checked &
            //            (
            //            ctrl_use.Checked & !ModifierKeys.HasFlag(Keys.Control)
            //            ))
            //        {
            //            for (int j = 0; j < selected_circles.Count; j++)
            //            {
            //                selected_circles[j].SetUnSelected();
            //            }
            //            selected_circles.Clear();
            //        }
            //        selected_circles.Add(circles[i]);
            //        circles[i].SetSelected();
            //        this.Invalidate(true);
            //        return;
            //    }
            //}
            //this.Update();
            //gOBJ obj;
            //if (e.X + 120 >= Width)
            //    return;
            //if (e.Y + 140 >= Height)
            //    return;
            //switch (comboBox1.SelectedIndex)
            //{
            //    case 0:
            //        obj = new CCircle(e.X, e.Y);
            //        break;
            //    case 1:
            //        obj = new CRect(e.X, e.Y);
            //        break;
            //    case 2:
            //        obj = new CLine(e.X, e.Y);
            //        break;
            //    default:
            //        return;

            //}

            //circles.Add(obj);
            //this.Invalidate(true);
            //return;

            if (e.Button != MouseButtons.Left)
                return;
            bool sel = false;
            for (int i = 0; i < circles.Count; i++)
            {
                if (circles[i].PointOnMe(e.X, e.Y))
                {
                    if (!MultiSelect.Checked |
                        MultiSelect.Checked &
                        (
                        ctrl_use.Checked & !ModifierKeys.HasFlag(Keys.Control)
                        ))
                    {
                        for (int j = 0; j < selected_circles.Count; j++)
                        {
                            selected_circles[j].SetUnSelected();
                        }
                        selected_circles.Clear();
                    }
                    selected_circles.Add(circles[i]);
                    circles[i].SetSelected();
                    this.Invalidate(true);
                    if (ctrl_use.Checked & ModifierKeys.HasFlag(Keys.Control))
                    {
                        return;
                    }
                    sel = true;
                }
            }
            if (sel)
                return;
            this.Update();
            gOBJ obj;
            if (e.X + 120 >= Width)
                return;
            if (e.Y + 140 >= Height)
                return;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    obj = new CCircle(e.X - 50, e.Y - 50);
                    break;
                case 1:
                    obj = new CRect(e.X - 50, e.Y - 50);
                    break;
                case 2:
                    obj = new CLine(e.X, e.Y);
                    break;
                default:
                    return;

            }

            circles.Add(obj);
            this.Invalidate(true);
            return;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selected_circles.Count == 0)
                return;
            return;
            /*            selected_circle.SetPos(e.X, e.Y);
                        selected_circle.SetSelected();
                        this.Invalidate(true);*/
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {

            }
            if (e.KeyCode == Keys.Escape)
            {
                for (int i = 0; i < selected_circles.Count; i++)
                {
                    selected_circles[i].SetUnSelected();
                }
                selected_circles.Clear();
            }

            if (e.KeyCode == Keys.Delete)
            {
                for (int i = 0; i < selected_circles.Count; i++)
                {
                    circles.Remove(selected_circles[i]);
                }
                selected_circles.Clear();
            }

            if (e.Control)
            {
                if (e.KeyCode == Keys.Left)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Width() - 1 <= 0)
                            continue;
                        selected_circles[i].SetWidth(selected_circles[i].Width() - 1);
                    }
                }

                if (e.KeyCode == Keys.Right)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].X() + selected_circles[i].Width() + 20 >= Width)
                            continue;
                        selected_circles[i].SetWidth(selected_circles[i].Width() + 1);
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Y() + selected_circles[i].Height() + 40 >= Height)
                            continue;
                        selected_circles[i].SetHeight(selected_circles[i].Height() + 1);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Height() - 1 <= 0)
                            continue;
                        selected_circles[i].SetHeight(selected_circles[i].Height() - 1);
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Left)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].X() - 1 < 0)
                            continue;
                        selected_circles[i].SetPos(selected_circles[i].X() - 1, selected_circles[i].Y());
                    }
                }

                if (e.KeyCode == Keys.Right)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].X() + selected_circles[i].Width() + 20 >= Width)
                            continue;
                        selected_circles[i].SetPos(selected_circles[i].X() + 1, selected_circles[i].Y());
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Y() + selected_circles[i].Height() + 40 >= Height)
                            continue;
                        selected_circles[i].SetPos(selected_circles[i].X(), selected_circles[i].Y() + 1);
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    for (int i = 0; i < selected_circles.Count; i++)
                    {
                        if (selected_circles[i].Y() - 1 < 0)
                            continue;
                        selected_circles[i].SetPos(selected_circles[i].X(), selected_circles[i].Y() - 1);
                    }
                }

            }
            this.Invalidate(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void clr_btn_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = _color;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                _color = MyDialog.Color;
                clr_btn.BackColor = _color;
                for (int i = 0; i < selected_circles.Count; i++)
                {
                    selected_circles[i].SetColor(_color);
                }
                this.Invalidate(true);
            }

        }

        private void make_grp_btn_Click(object sender, EventArgs e)
        {
            if (selected_circles.Count < 1)
                return;
            CGroup group = new CGroup(selected_circles);
            for (int i = 0; i < selected_circles.Count; i++)
            {
                circles.Remove(selected_circles[i]);
                selected_circles[i].SetUnSelected();
            }
            selected_circles.Clear();
            group.SetSelected();
            selected_circles.Add(group);
            circles.Add(group);
            this.Invalidate(true);
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("root");
            xml.AppendChild(root);
            for (int i = 0; i < circles.Count; i++)
            {
                XmlElement el = root.OwnerDocument.CreateElement("obj");
                root.AppendChild(el);
                circles[i].Save(el);
            }

            xml.Save("test.xml");
        }

        private void Load_btn_Click(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("test.xml");
            circles.Clear();
            gOBJ obj;
            for (int i = 0; i < xml.LastChild.ChildNodes.Count; i++)
            {
                switch (xml.LastChild.ChildNodes[i].Attributes["Type"].Value)
                {
                    case "Circle":
                        obj = new CCircle(0, 0);
                        obj.Load(xml.LastChild.ChildNodes[i]);
                        circles.Add(obj);
                        break;
                    case "Rect":
                        obj = new CRect(0, 0);
                        obj.Load(xml.LastChild.ChildNodes[i]);
                        circles.Add(obj);
                        break;
                    case "Group":
                        obj = new CGroup();
                        obj.Load(xml.LastChild.ChildNodes[i]);
                        circles.Add(obj);
                        break;
                    case "Line":
                        obj = new CLine(0, 0);
                        obj.Load(xml.LastChild.ChildNodes[i]);
                        circles.Add(obj);
                        break;
                }
            }
            this.Invalidate(true);
        }
    }
}