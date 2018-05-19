Imports System.Collections.Generic
Imports DevExpress.Pdf
Imports System.Linq

Namespace GetRadioGroupCheckedValues
    Friend Class Program

        Shared Sub Main(ByVal args() As String)

            ' Load a document with an interactive form.
            Dim processor As New PdfDocumentProcessor()
            processor.LoadDocument("DocumentToFill.pdf")

            ' Obtain interactive form data from a document.
            Dim formData As PdfFormData = processor.GetFormData()

            ' Obtain the radio group checked appearance names.
            Dim radioGroupCheckedValues As List(Of String) = GetRadioGroupCheckedAppearanceNames(processor, "Gender")

            ' Specify a checked appearance name for the Female radio button.
            formData("Gender").Value = radioGroupCheckedValues(0)

            ' Apply data to the interactive form. 
            processor.ApplyFormData(formData)

            ' Save the modified document.
            processor.SaveDocument("..\..\Result.pdf")
        End Sub

        Private Shared Function GetRadioGroupCheckedAppearanceNames(ByVal processor As PdfDocumentProcessor, ByVal fieldName As String) As List(Of String)
            If processor.Document Is Nothing OrElse processor.Document.AcroForm Is Nothing Then
                Return Nothing
            End If
            Dim button As PdfInteractiveFormField = FindRadioButton(processor.Document.AcroForm.Fields, "", fieldName)
            If button IsNot Nothing Then
                Dim result As New List(Of String)()
                For Each kid As PdfInteractiveFormField In button.Kids
                    If kid.Widget IsNot Nothing AndAlso kid.Widget.Appearance IsNot Nothing Then
                        Dim names As List(Of String) = kid.Widget.Appearance.Down.Forms.Keys.ToList()

                        If names.Count = 1 Then
                            result.Add(names(0))
                        End If
                        If names.Count > 1 Then
                            If names(0) = "Off" Then
                                result.Add(names(1))
                            Else
                                result.Add(names(0))
                            End If
                        End If
                    End If
                Next kid
                Return result
            End If
            Return Nothing
        End Function

        Private Shared Function FindRadioButton(ByVal fields As IEnumerable(Of PdfInteractiveFormField), ByVal partialName As String, ByVal fieldName As String) As PdfInteractiveFormField
            For Each field As PdfInteractiveFormField In fields
                If partialName & field.Name = fieldName Then
                    If field.Flags.HasFlag(PdfInteractiveFormFieldFlags.Radio) Then
                        Return TryCast(field, PdfButtonFormField)
                    End If
                    Return Nothing
                End If
                If field.Kids IsNot Nothing Then
                    Dim intermediateResult As PdfInteractiveFormField = FindRadioButton(field.Kids, partialName & "." & field.Name, fieldName)
                    If intermediateResult IsNot Nothing Then
                        Return intermediateResult
                    End If
                End If
            Next field
            Return Nothing
        End Function
    End Class
End Namespace
