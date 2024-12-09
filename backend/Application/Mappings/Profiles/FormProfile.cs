using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mappings.Profiles.Base;
using SiradigCalc.Core.Entities.Forms;

namespace SiradigCalc.Application.Mappings.Profiles;

public abstract class FormProfile() : BaseProfile<Form, FormDto>()
{
}