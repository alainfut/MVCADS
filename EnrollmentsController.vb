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
    Public Class EnrollmentsController
        Inherits System.Web.Mvc.Controller

        Private db As New ContosoUniversityDataEntities

        ' GET: Enrollments
        Function Index() As ActionResult
            Dim enrollment = db.Enrollment.Include(Function(e) e.Course).Include(Function(e) e.Student)
            Return View(enrollment.ToList())
        End Function

        ' GET: Enrollments/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim enrollment As Enrollment = db.Enrollment.Find(id)
            If IsNothing(enrollment) Then
                Return HttpNotFound()
            End If
            Return View(enrollment)
        End Function

        ' GET: Enrollments/Create
        Function Create() As ActionResult
            ViewBag.CourseID = New SelectList(db.Course, "CourseID", "Materia")
            ViewBag.StudentID = New SelectList(db.Student, "StudentID", "Apellido")
            Return View()
        End Function

        ' POST: Enrollments/Create
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        'más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="EnrollmentID,CourseID,StudentID,Nota")> ByVal enrollment As Enrollment) As ActionResult
            If ModelState.IsValid Then
                db.Enrollment.Add(enrollment)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.CourseID = New SelectList(db.Course, "CourseID", "Materia", enrollment.CourseID)
            ViewBag.StudentID = New SelectList(db.Student, "StudentID", "Apellido", enrollment.StudentID)
            Return View(enrollment)
        End Function

        ' GET: Enrollments/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim enrollment As Enrollment = db.Enrollment.Find(id)
            If IsNothing(enrollment) Then
                Return HttpNotFound()
            End If
            ViewBag.CourseID = New SelectList(db.Course, "CourseID", "Materia", enrollment.CourseID)
            ViewBag.StudentID = New SelectList(db.Student, "StudentID", "Apellido", enrollment.StudentID)
            Return View(enrollment)
        End Function

        ' POST: Enrollments/Edit/5
        'Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        'más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="EnrollmentID,CourseID,StudentID,Nota")> ByVal enrollment As Enrollment) As ActionResult
            If ModelState.IsValid Then
                db.Entry(enrollment).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.CourseID = New SelectList(db.Course, "CourseID", "Materia", enrollment.CourseID)
            ViewBag.StudentID = New SelectList(db.Student, "StudentID", "Apellido", enrollment.StudentID)
            Return View(enrollment)
        End Function

        ' GET: Enrollments/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim enrollment As Enrollment = db.Enrollment.Find(id)
            If IsNothing(enrollment) Then
                Return HttpNotFound()
            End If
            Return View(enrollment)
        End Function

        ' POST: Enrollments/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim enrollment As Enrollment = db.Enrollment.Find(id)
            db.Enrollment.Remove(enrollment)
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
