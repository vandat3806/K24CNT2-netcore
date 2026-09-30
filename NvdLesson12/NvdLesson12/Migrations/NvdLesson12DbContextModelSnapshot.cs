using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using NvdLesson12.Data;

#nullable disable

namespace NvdLesson12.Migrations;

[DbContext(typeof(NvdLesson12DbContext))]
partial class NvdLesson12DbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder) =>
        NvdLesson12ModelSnapshotBuilder.Build(modelBuilder);
}
