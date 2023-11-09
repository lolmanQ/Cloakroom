using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloakroomSharp
{
	abstract class ProjectTemplate
	{
		public abstract bool Run(string aPath, string aAssignmentName);
	}
}
