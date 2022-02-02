Imports DevExpress.Pdf

Namespace GetRadioGroupCheckedValues

    Friend Class Program

        Shared Sub Main(ByVal args As String())
            ' Load a document with an interactive form.
            Dim processor As PdfDocumentProcessor = New PdfDocumentProcessor()
            processor.LoadDocument("DocumentToFill.pdf")
            ' Retrieve the form field facade:
            Dim documentFacade As PdfDocumentFacade = processor.DocumentFacade
            Dim acroFormFacade As PdfAcroFormFacade = documentFacade.AcroForm
            ' Specify a checked appearance name for the Female radio button:
            Dim genderField As PdfRadioGroupFormFieldFacade = acroFormFacade.GetRadioGroupFormField("Gender")
            For Each item As PdfFormFieldItem In genderField.Field.Items
                If Equals(item.Value, "Female") Then genderField.Value = item.Value
            Next

            ' Save the modified document.
            processor.SaveDocument("..\..\Result.pdf")
        End Sub
    End Class
End Namespace
