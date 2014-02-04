using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARS_Expert.ResourceManager
{
    public class Etape
    {

        private int id;
        private String name;
        private String description;
        private Procedure procedure;
        private List<Object3d> objects3d;

        public Etape()
        {
            try
            {
                this.id = Manager.XMLResourceManager.XMLStep.getNextId();
            }
            catch (Exception x) { };
            objects3d = new List<Object3d>();
        }
        public Etape(int id, String name, String description, Procedure procedure)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.procedure = procedure;
            objects3d = new List<Object3d>();
        }
        
        public int getId()
        {
            return id;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        
        public String getName()
        {
            return name;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        
        public String getDescription()
        {
            return description;
        }
        public void setDescription(String description)
        {
            this.description = description;
        }
        
        public Procedure getprocedure()
        {
            return procedure;
        }
        public void setprocedure(Procedure procedure)
        {
            this.procedure = procedure;
        }

        public void addObject3d(Object3d object3d)
        {
            object3d.setStep(this);
            objects3d.Add(object3d);
        }

        public void removeObjects3d(int index)
        {
            //System.Windows.Forms.MessageBox.Show("Objects.count: " + objects3d.Count + ". index: " + index, "Etape.removeObject");
            objects3d[index].setStep(null);
            objects3d.RemoveAt(index);
        }

        public void removeObjects3d(Object3d object3d)
        {
            object3d.setStep(null); ;
            objects3d.Remove(object3d);
        }

        public int objectsCount()
        {
            return objects3d.Count;
        }

        public Object3d getObject(int index)
        {
            return objects3d[index];
        }

        public List<Object3d> getObjectList()
        {
            return objects3d;
        }

        public void setObjectList(List<Object3d> objects)
        {
            this.objects3d = objects;
        }

        public bool AddStepToDB()
        {
            return false;
            //return Manager.XMLResourceManager.XMLStep.AddStepOnly(this);
        }

        /// <summary>
        /// Add to Data base.
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            return Manager.XMLResourceManager.XMLStep.Add(this);
        }

        public bool Update(int id)
        {
            return Manager.XMLResourceManager.XMLStep.Update(id, this);
        }

        public int GetNextId()
        {
            return Manager.XMLResourceManager.XMLStep.getNextId();
        }

        public bool Exists()
        {
            bool ret = false;
            try
            {
                ret = Manager.XMLResourceManager.XMLStep.Exists(this.id);
            }
            catch (System.IO.FileNotFoundException) { return false; }
            catch (Exception x)
            {
                //System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }

        public bool AddStep_ObjectsToDB()
        {
            return false;
            //return Manager.XMLResourceManager.XMLStep.AddStep_Objects(this);
        }

        public static Etape GetByID(int id)
        {
            Etape ret = null;
            try
            {
                ret = Manager.XMLResourceManager.XMLStep.GetById(id);
            }
            catch (System.IO.FileNotFoundException) { return null; }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }

        public static List<Etape> GetByProcedure(int id)
        {
            List<Etape> ret = null;
            try
            {
                ret = Manager.XMLResourceManager.XMLStep.GetByProcedure(id);
            }
            catch (System.IO.FileNotFoundException) { return null; }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }

        public static bool RemoveByProcedure(int id)
        {
            bool ret = true;
            try
            {
                ret = Manager.XMLResourceManager.XMLStep.RemoveByProcedure(id);
            }
            catch (System.IO.FileNotFoundException) { return false; }
            catch (Exception x)
            {
                System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }
    }

}
