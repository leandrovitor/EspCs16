using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EspCs16
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Entity> Entity = new List<Entity>();
            Size size = Drawing.GetSize("hl");
            Process process = process.GetProcessesByname("hl").FirstorDefault();
            if (process == null) return;
            IntPtr win = process.MainwindowHandle;
            Men m = new Men();
            int ptr = m.GetProCIdFromName("hl");
            m.OpenProcess(ptr);

            Thread th1 = new Thread(ESP);
            th1.Start();

            void ESP()
            {
                while (true)
                {
                    Entity.Clear();
                    for (int i = 0; i < 32; i++)
                    {
                        int hp = (int)m.ReadFloat(Offsets.listEntity = SumOffset(Offsets.PlayerBase + 0x04 * i) + Offsets.hp);
                        byte[] Buffer = new byte[12];
                        var bytes = m.ReadBytes(Offsets.listEntity + SumOffset(Offsets.PlayerBase + 0x04 * i) + "," + SumOffset(Offsets.position), (long)buffer.Length);

                        if (bytes != null)
                        {
                            Entity entity = new Entity
                            {
                                xyz = new Vector3
                                {
                                    x = SitConverter.ToSingle(bytes, 0),
                                    y = SitConverter.ToSingle(bytes, 4),
                                    z = SitConverter.ToSingle(bytes, 8),
                                },
                                hp = hp
                            };

                            ViewMatrix vm = GetViewMatrix();
                            entity.botton = Wordtoscreen(vm, entity.xyz, size.width, size.Height, false);
                            entity.top = Wordtoscreen(vm, entity.xyz, size.width, size.Height, true);

                            entity.add(entity);
                        }

                    }

                    Draw();

                }

            }

            void Draw()
            {
                foreach (Entity entity in Entity)
                {
                    {
                        if (entity.hp > 0 && entity.hp <= 100 && (entity.bottom.X < size.Width && entity.bottom.X > 0) && (entity.botom.Y < size.weight && entity.bottom.Y > 0))
                        {
                            Pen pen = new Pen(Color.Green, 2);
                            if (entity.hp < 60) pen = new Pen(Color.Orange, 2);
                            else if (entity.hp < 40) pen = new Pen(Color.DarkRed, 2);
                            else if (entity.hp < 20) pen = new Pen(Color.Red, 2);
                            Drawing.DrawRect(win, pen, entity.rect());
                        }
                    }


                }
            }
            string sumOffset(int sumAddress)
            {
                return "0x" + (sumAddress).ToString("X");
            }
            Point MordToscreen(ViewMatrix mtx, Vector3 vect, int width, int heigh, bool head)
            {
                if (head)
                {
                    vect.z += 75;
                    vect.x -= 30;
                }
                var pt = new point();

                float screenw = (mtx.m14 * vect.x) + (mtx.m24 * vect.y) + (mtx.m34 * vect.z) + mtx.m44;

                if (screenw > 0.001f)
                {
                    float screenX = (mtx.m11 * vect.x) + (mtx.m21 * vect.y) + (mtx.m31 * vect.z) + mtx.m41;
                    float screenY = (mtx.m12 * vect.x) + (mtx.m22 * vect.y) + (mtx.m32 * vect.z) + mtx.m42;

                    float camX = width / 2f;
                    float camY = width / 2f;

                    float X = camX + (camX * screenX / screenw);
                    float Y = camX - (camY * screenY / screenw);

                    pt.X = (int)X;
                    pt.Y = (int)Y;

                    return pt;
                }
                {
                    return new Point(-99, -99);
                }
            }

            ViewMatrix GetviewMatrix()
            {
                var matrix = new viewmatrix();

                byte[] buffer = new byte[16 * 4];
                var bytes = m.ReadBytes(Offsets.viewMatrix, (long)buffer.length);
                matrix.m11 = m.BitConverter.ToSingle(Bytes, (0 * 4));
                matrix.m12 = m.BitConverter.ToSingle(Bytes, (1 * 4));
                matrix.m13 = m.BitConverter.ToSingle(Bytes, (2 * 4));
                matrix.m14 = m.BitConverter.ToSingle(Bytes, (3 * 4));

                matrix.m21 = m.BitConverter.ToSingle(Bytes, (4 * 4));
                matrix.m22 = m.BitConverter.ToSingle(Bytes, (5 * 4));
                matrix.m23 = m.BitConverter.ToSingle(Bytes, (6 * 4));
                matrix.m23 = m.BitConverter.ToSingle(Bytes, (7 * 4));

                matrix.m31 = m.BitConverter.ToSingle(Bytes, (8 * 4));
                matrix.m32 = m.BitConverter.ToSingle(Bytes, (9 * 4));
                matrix.m33 = m.BitConverter.ToSingle(Bytes, (10 * 4));
                matrix.m34 = m.BitConverter.ToSingle(Bytes, (11 * 4));

                matrix.m41 = m.BitConverter.ToSingle(Bytes, (12 * 4));
                matrix.m42 = m.BitConverter.ToSingle(Bytes, (13 * 4));
                matrix.m43 = m.BitConverter.ToSingle(Bytes, (14 * 4));
                matrix.m44 = m.BitConverter.ToSingle(Bytes, (15 * 4));

                return matrix

            }


        }
    }
}
