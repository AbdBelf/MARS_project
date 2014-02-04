using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MARS_Expert.ResourceManager
{
    public class TypePanne
    {

        private int id;
        private String name;
        private String description;
        private List<Panne> pannes = new List<Panne>();

        public TypePanne()
        {
            try
            {
                this.id = Manager.XMLResourceManager.XMLFailureType.getNextId();
            }
            catch (Exception x) {/*id is zero in this case*/};
        }
        public TypePanne(int id, String name, String description)
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

        public void addPanne(Panne panne)
        {
            panne.setTypePanne(this);
            pannes.Add(panne);
        }

        public void removePanne(int index)
        {
            pannes[index].setTypePanne(this);
            pannes.RemoveAt(index);
        }

        public void removePanne(Panne panne)
        {
            panne.setTypePanne(null);
            pannes.Remove(panne);
        }

        public int pannesCount()
        {
            return pannes.Count;
        }

        public Panne getPanne(int index)
        {
            return pannes[index];
        }

        public List<Panne> getPannes()
        {
            return pannes;
        }

        /// <summary>
        /// Add new TypePanne element to the database.
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            return Manager.XMLResourceManager.XMLFailureType.Add(this);
        }

        public int GetNExtId()
        {
            return Manager.XMLResourceManager.XMLFailureType.getNextId();
        }

        public bool Exists()
        {
            bool ret = false;
            try
            {
                ret = Manager.XMLResourceManager.XMLFailureType.Exists(this.id);
            }
            catch (System.IO.FileNotFoundException) { return false; }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }

        public static TypePanne GetByID(int id)
        {
            TypePanne ret = null;
            try
            {
                ret = Manager.XMLResourceManager.XMLFailureType.GetById(id);
            }
            catch (System.IO.FileNotFoundException) { return null; }
            catch (Exception x) {// System.Windows.Forms.MessageBox.Show(x.ToString());
            }
            return ret;
        }

        public bool Update(int id)
        {
            return Manager.XMLResourceManager.XMLFailureType.Update(id, this);
        }

        public override string ToString()
        {
            return this.name;
        }
    }

}
