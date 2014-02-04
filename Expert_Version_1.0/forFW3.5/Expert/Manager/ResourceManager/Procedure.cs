using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARS_Expert.ResourceManager
{
    public class Procedure
    {

        private int id;
        private String name;
        private String description;
        private Panne panne;
        public List<Etape> steps = new List<Etape>();

        public Procedure()
        {
            try
            {
                this.id = Manager.XMLResourceManager.XMLProcedure.getNextId();
            }
            catch (Exception) { };
        }

        public Procedure(int id, String name, String description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public Procedure(int id, String name, String description, Panne panne)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.panne = panne;
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
        
        public Panne getPanne()
        {
            return panne;
        }
        public void setPanne(Panne panne)
        {
            this.panne = panne;
        }

        public void addEtape(Etape etape)
        {
            etape.setprocedure(this);
            steps.Add(etape);
        }

        public void removeEtape(int index)
        {
            steps[index].setprocedure(null);
            steps.RemoveAt(index);
        }

        public void removeEtape(Etape etape)
        {
            etape.setprocedure(null);
            steps.Remove(etape);
        }

        public int etapesCount()
        {
            return steps.Count;
        }

        public Etape getEtape(int index)
        {
            return steps[index];
        }

        public List<Etape> getEtapes()
        {
            return steps;
        }

        public void setEtapes(List<Etape> etapeList)
        {
            this.steps = etapeList;
        }

        public bool AddToDB()
        {
            return Manager.XMLResourceManager.XMLProcedure.Add(this);
        }

        public bool Update(int id)
        {
            return Manager.XMLResourceManager.XMLProcedure.Update(id, this);
        }

        public int GetNextId()
        {
            return Manager.XMLResourceManager.XMLProcedure.getNextId();
        }

        public bool Exists()
        {
            bool ret = false;
            try
            {
                ret = Manager.XMLResourceManager.XMLProcedure.Exists(this.id);
            }
            catch (System.IO.FileNotFoundException x) { return false; }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }

        public static Procedure GetByID(int id)
        {
            Procedure ret = null;
            try
            {
                ret = Manager.XMLResourceManager.XMLProcedure.GetById(id);
            }
            catch (System.IO.FileNotFoundException) { return null; }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }

        public override string ToString()
        {
            return this.name;
        }
    }

}
