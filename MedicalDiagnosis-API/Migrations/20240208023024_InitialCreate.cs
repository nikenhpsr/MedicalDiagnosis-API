using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalDiagnosis_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    SpecialityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.SpecialityId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialityId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "SpecialityId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentAuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true),
                    ParentCommentCommentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentCommentId",
                        column: x => x.ParentCommentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Comments_Doctors_CommentAuthorId",
                        column: x => x.CommentAuthorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId");
                });

            migrationBuilder.CreateTable(
                name: "MedicalInspections",
                columns: table => new
                {
                    MedicalInspectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InspectionDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Anamnesis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complaints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recommendations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextVisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Conclusion = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentInspectionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MedicalInspectionId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInspections", x => x.MedicalInspectionId);
                    table.CheckConstraint("CK_MedicalInspection_Conclusion_DeathDate", "([Conclusion] = 'Death' AND [DeathDate] IS NOT NULL) OR ([Conclusion] <> 'Death')");
                    table.CheckConstraint("CK_MedicalInspection_Conclusion_NextVisit", "([Conclusion] = 'Disease' AND [NextVisitDate] IS NOT NULL) OR ([Conclusion] <> 'Disease')");
                    table.CheckConstraint("CK_MedicalInspection_MainDiagnosis", "SELECT COUNT(*) FROM Diagnoses WHERE TypeInInspection = 'Main' AND MedicalInspectionId = Id GROUP BY MedicalInspectionId HAVING COUNT(*) = 1");
                    table.ForeignKey(
                        name: "FK_MedicalInspections_Doctors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalInspections_MedicalInspections_MedicalInspectionId1",
                        column: x => x.MedicalInspectionId1,
                        principalTable: "MedicalInspections",
                        principalColumn: "MedicalInspectionId");
                    table.ForeignKey(
                        name: "FK_MedicalInspections_MedicalInspections_ParentInspectionId",
                        column: x => x.ParentInspectionId,
                        principalTable: "MedicalInspections",
                        principalColumn: "MedicalInspectionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalInspections_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    ConsultationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalInspectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicalInspectionsMedicalInspectionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.ConsultationId);
                    table.ForeignKey(
                        name: "FK_Consultations_MedicalInspections_MedicalInspectionId",
                        column: x => x.MedicalInspectionId,
                        principalTable: "MedicalInspections",
                        principalColumn: "MedicalInspectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultations_MedicalInspections_MedicalInspectionsMedicalInspectionId",
                        column: x => x.MedicalInspectionsMedicalInspectionId,
                        principalTable: "MedicalInspections",
                        principalColumn: "MedicalInspectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    DiagnosesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MedicalInspectionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.DiagnosesId);
                    table.ForeignKey(
                        name: "FK_Diagnoses_MedicalInspections_MedicalInspectionId",
                        column: x => x.MedicalInspectionId,
                        principalTable: "MedicalInspections",
                        principalColumn: "MedicalInspectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalInspectionDiagnosis",
                columns: table => new
                {
                    MedicalInspectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiagnosesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicalInspectionDiagnosisId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInspectionDiagnosis", x => new { x.MedicalInspectionId, x.DiagnosesId });
                    table.ForeignKey(
                        name: "FK_MedicalInspectionDiagnosis_Diagnoses_DiagnosesId",
                        column: x => x.DiagnosesId,
                        principalTable: "Diagnoses",
                        principalColumn: "DiagnosesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalInspectionDiagnosis_MedicalInspections_MedicalInspectionId",
                        column: x => x.MedicalInspectionId,
                        principalTable: "MedicalInspections",
                        principalColumn: "MedicalInspectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentAuthorId",
                table: "Comments",
                column: "CommentAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentCommentId",
                table: "Comments",
                column: "ParentCommentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_MedicalInspectionId",
                table: "Consultations",
                column: "MedicalInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_MedicalInspectionsMedicalInspectionId",
                table: "Consultations",
                column: "MedicalInspectionsMedicalInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_MedicalInspectionId",
                table: "Diagnoses",
                column: "MedicalInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialityId",
                table: "Doctors",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInspectionDiagnosis_DiagnosesId",
                table: "MedicalInspectionDiagnosis",
                column: "DiagnosesId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInspections_AuthorId",
                table: "MedicalInspections",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInspections_MedicalInspectionId1",
                table: "MedicalInspections",
                column: "MedicalInspectionId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInspections_ParentInspectionId",
                table: "MedicalInspections",
                column: "ParentInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalInspections_PatientId",
                table: "MedicalInspections",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropTable(
                name: "MedicalInspectionDiagnosis");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "MedicalInspections");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Specialities");
        }
    }
}
