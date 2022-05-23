using BakeSchoolAdmin_Models;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.Events
{
    /// <summary>
    /// Reload the data of category on the main page
    /// </summary>
    public class ReloadCategoryDataEvent : CompositePresentationEvent<bool>
    {
    }
}
