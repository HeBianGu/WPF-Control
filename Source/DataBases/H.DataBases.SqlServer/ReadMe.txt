���ݿ�Ǩ������
����һ��������´��룬�������ʱ DbContext ����
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-UQVBO72\\SQLEXPRESS; Trusted_Connection=False; uid=sa; pwd=123456;Initial Catalog=PerformanceMigration; MultipleActiveResultSets=true;");

            return new DataContext(optionsBuilder.Options);
        }
    }

���������֤��������������ɳɹ�

��������DbContext������ڵ�ǰ��������

�����������������
H.DataBases.SqlServer

��������ִ��Ǩ���������Ǩ���ļ�
add-migration init -project H.DataBases.SqlServer

�����ģ�ִ�и������ݿ����ͬ�������ݿ���
Update-Database -project H.DataBases.SqlServer

���������Զ�ִ��Ǩ�ƣ��滻�������ݿⷽ��
db.Database.Migrate();�滻�� db.Database.EnsureCreated();


�����ӳټ��أ��Ȳ�����ʾ����include������Ĭ�ϼ���������
��װ��
Microsoft.EntityFrameworkCore.Proxies

����UseLazyLoadingProxies
optionsBuilder.UseLazyLoadingProxies().UseSqlite(connect);

�������
modelBuilder.Entity<im_dd_music>().HasMany(x => x.Images).WithOne(x => x.Music).HasForeignKey(x => x.MusicID).OnDelete(DeleteBehavior.Cascade);

����virtual
public virtual ObservableCollection<im_dd_image> Images { get; set; } = new ObservableCollection<im_dd_image>();


