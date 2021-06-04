# How to obtain a checked appearance name for each radio button in the radio group


<b>Note:</b> Starting with 21.1, the <b>PdfDocumentFacade</b> class allows you to change the PDF document without access to its inner structure. Use the <b>PdfDocumentFacade.AcroForm</b> property to get interactive form field options. You can change form field and appearance properties.

This example shows how to get a checked appearance name for each radio button and check the corresponding radio button with the obtained value.<br>To accomplish this task, call the <strong>GetRadioGroupCheckedAppearanceNames</strong> method using a <strong>PdfDocumentProcessor</strong> instance and the field. <br><br>To obtain interactive form data, call the <a href="https://documentation.devexpress.com/DocumentServer/DevExpress.Pdf.PdfDocumentProcessor.GetFormData.method">PdfDocumentProcessor.GetFormData </a>method. To check the "Female" radio button, use the <a href="https://documentation.devexpress.com/CoreLibraries/DevExpress.Pdf.PdfFormData.Value.property">PdfFormData.Value</a> property.

<br/>


