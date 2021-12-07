Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ConsontoSite

Namespace Controllers
    Public Class StudentsController
        Inherits System.Web.Mvc.Controller

        Private db As New ContosoUniversityDataEntities

        ' GET: Students
        Function Index() As ActionResult
            Return View(db.Student.ToList())
        End Function

        ' GET: Students/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim student As Student = db.Student.Find(id)
            If IsNothing(student) Then
                Return HttpNotFound()
            End If
            Return View(student)
        End Function

        ' GET: Students/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Students/Create
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        'más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="StudentID,Apellido,Nombre,Inscripcion")> ByVal student As Student) As ActionResult
            If ModelState.IsValid Then
                db.Student.Add(student)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(student)
        End Function

        ' GET: Students/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim student As Student = db.Student.Find(id)
            If IsNothing(student) Then
                Return HttpNotFound()
            End If
            Return View(student)
        End Function

        ' POST: Students/Edit/5
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        'más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="StudentID,Apellido,Nombre,Inscripcion")> ByVal student As Student) As ActionResult
            If ModelState.IsValid Then
                db.Entry(student).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(student)
        End Function

        ' GET: Students/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim student As Student = db.Student.Find(id)
            If IsNothing(student) Then
                Return HttpNotFound()
            End If
            Return View(student)
        End Function

        ' POST: Students/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim student As Student = db.Student.Find(id)
            db.Student.Remove(student)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
