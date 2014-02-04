using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARS_Expert.ResourceManager
{
    public class Panne
    {

        private int id;
        private String name;
        private String description;
        private TypePanne typepanne;
        private List<Procedure> procedures = new List<Procedure>();

        public Panne()
        {
            try
            {
                this.id = Manager.XMLResourceManager.XMLFailure.getNextId();
            }
            catch (Exception) { };
        }

        public Panne(int id, String name, String description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
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

        public TypePanne getTypePanne()
        {
            return typepanne;
        }
        public void setTypePanne(TypePanne typePanne)
        {
            this.typepanne = typePanne;
        }

        public void addProcedure(Procedure procedure)
        {
            procedure.setPanne(this);
            procedures.Add(procedure);
        }

        public void removeProcedure(int index)
        {
            procedures[index].setPanne(null);
            procedures.RemoveAt(index);
        }

        public void removeProcedure(Procedure procedure)
        {
            procedure.setPanne(null);
            procedures.Remove(procedure);
        }

        public int proceduresCount()
        {
            return procedures.Count;
        }

        public Procedure getProcedure(int index)
        {
            return procedures[index];
        }

        public List<Procedure> getProcedures()
        {
            return procedures;
        }

        /// <summary>
        /// Add new Panne element to the database.
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            return Manager.XMLResourceManager.XMLFailure.Add(this);
        }

        public int GetNExtId()
        {
            return Manager.XMLResourceManager.XMLFailure.getNextId();
        }

        public bool Exists()
        {
            bool ret = false;
            try
            {
                ret = Manager.XMLResourceManager.XMLFailure.Exists(this.id);
            }
            catch (System.IO.FileNotFoundException) { return false; }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString()); 
            }
            return ret;
        }

        public static Panne GetByID(int id)
        {
            Panne ret = null;
            try
            {
                ret = Manager.XMLResourceManager.XMLFailure.GetById(id);
            }
            catch (System.IO.FileNotFoundException) { return null; }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString()); 
            }
            return ret;
        }

        public bool Update(int id)
        {
            return Manager.XMLResourceManager.XMLFailure.Update(id, this);
        }

        public override string ToString()
        {
            return this.name;
        }

    }

}
