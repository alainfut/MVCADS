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
    Public Class CoursesController
        Inherits System.Web.Mvc.Controller

        Private db As New ContosoUniversityDataEntities

        ' GET: Courses
        Function Index() As ActionResult
            Return View(db.Course.ToList())
        End Function

        ' GET: Courses/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim course As Course = db.Course.Find(id)
            If IsNothing(course) Then
                Return HttpNotFound()
            End If
            Return View(course)
        End Function

        ' GET: Courses/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Courses/Create
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        'más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="CourseID,Credits,Materia")> ByVal course As Course) As ActionResult
            If ModelState.IsValid Then
                db.Course.Add(course)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(course)
        End Function

        ' GET: Courses/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim course As Course = db.Course.Find(id)
            If IsNothing(course) Then
                Return HttpNotFound()
            End If
            Return View(course)
        End Function

        ' POST: Courses/Edit/5
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        'más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="CourseID,Credits,Materia")> ByVal course As Course) As ActionResult
            If ModelState.IsValid Then
                db.Entry(course).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(course)
        End Function

        ' GET: Courses/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim course As Course = db.Course.Find(id)
            If IsNothing(course) Then
                Return HttpNotFound()
            End If
            Return View(course)
        End Function

        ' POST: Courses/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim course As Course = db.Course.Find(id)
            db.Course.Remove(course)
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
