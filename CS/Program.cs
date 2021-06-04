using DevExpress.Pdf;

namespace GetRadioGroupCheckedValues
{
    class Program
    {

        static void Main(string[] args)
        {
            // Load a document with an interactive form.
            PdfDocumentProcessor processor = new PdfDocumentProcessor();
            processor.LoadDocument(@"DocumentToFill.pdf");

            // Retrieve the form field facade:
            PdfDocumentFacade documentFacade = processor.DocumentFacade;
            PdfAcroFormFacade acroFormFacade = documentFacade.AcroForm;

            // Specify a checked appearance name for the Female radio button:
            PdfRadioGroupFormFieldFacade genderField = acroFormFacade.GetRadioGroupFormField("Gender");
            foreach (PdfFormFieldItem item in genderField.Field.Items)
            {
                if (item.Value == "Female")
                    genderField.Value = item.Value;
            }

            // Save the modified document.
            processor.SaveDocument("..\\..\\Result.pdf");
        }
    }
}
