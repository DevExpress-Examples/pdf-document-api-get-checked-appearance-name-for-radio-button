using System.Collections.Generic;
using DevExpress.Pdf;
using System.Linq;

namespace GetRadioGroupCheckedValues {
    class Program {

        static void Main(string[] args) {

            // Load a document with an interactive form.
            PdfDocumentProcessor processor = new PdfDocumentProcessor();
            processor.LoadDocument(@"DocumentToFill.pdf");

            // Obtain interactive form data from a document.
            PdfFormData formData = processor.GetFormData();

            // Obtain the radio group checked appearance names.
            List<string> radioGroupCheckedValues = GetRadioGroupCheckedAppearanceNames(processor, "Gender");

            // Specify a checked appearance name for the Female radio button.
            formData["Gender"].Value = radioGroupCheckedValues[0];

            // Apply data to the interactive form. 
            processor.ApplyFormData(formData);

            // Save the modified document.
            processor.SaveDocument("..\\..\\Result.pdf");
        }

        static List<string> GetRadioGroupCheckedAppearanceNames(PdfDocumentProcessor processor, string fieldName) {
            if (processor.Document == null || processor.Document.AcroForm == null)
                return null;
            PdfInteractiveFormField button = FindRadioButton(processor.Document.AcroForm.Fields, "", fieldName);
            if (button != null) {
                List<string> result = new List<string>();
                foreach (PdfInteractiveFormField kid in button.Kids) {
                    if (kid.Widget != null && kid.Widget.Appearance != null) {
                        List<string> names = kid.Widget.Appearance.Down.Forms.Keys.ToList();

                        if (names.Count == 1)
                            result.Add(names[0]);
                        if (names.Count > 1) {
                            if (names[0] == "Off")
                                result.Add(names[1]);
                            else result.Add(names[0]);
                        }
                    }
                }
                return result;
            }
            return null;
        }

        static PdfInteractiveFormField FindRadioButton(IEnumerable<PdfInteractiveFormField> fields, string partialName, string fieldName) {
            foreach (PdfInteractiveFormField field in fields) {
                if (partialName + field.Name == fieldName) {
                    if (field.Flags.HasFlag(PdfInteractiveFormFieldFlags.Radio))
                        return field as PdfButtonFormField;
                    return null;
                }
                if (field.Kids != null) {
                    PdfInteractiveFormField intermediateResult = FindRadioButton(field.Kids, partialName + "." + field.Name, fieldName);
                    if (intermediateResult != null)
                        return intermediateResult;
                }
            }
            return null;
        }
    }
}
