using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeSchoolAdmin_Gui.Events.CategoryEvents
{
    /// <summary>
    /// Event for the state of saving by the category
    /// </summary>
    public class IsSavedCategoryEvent : CompositePresentationEvent<string>
    {
    }
}
