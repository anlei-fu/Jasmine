using Dapper;
using Jasmine.Orm;
using System;
using System.Collections.Generic;

namespace OrmTest
{
    public class Test
    {

        private List<Animal> _datas;
        protected IDbConnectionProvider _provider;
        protected JasmineSqlExcutor _excutor;
        
        public void Run(IDbConnectionProvider provider,List<Animal> datas)
        {
            _provider = provider;
            _excutor = new JasmineSqlExcutor(_provider);
            _datas = datas;

            testSingleRowInsert();
            testSingleRowInsertPartial();
            testBulkInsert();
            testBulKInsertPartial();
            testDelete();
            testFullQuery();
            testFullQueryConditional();
            testPartialQuery();
            testPartialQuery1();
            testUpdate();
            testUpdateBulk();

        }

        protected void doTest(string title, Action dapper, Action excutor, bool clearData = false)
        {

            Utils.PrintTitle(title);

            Utils.ResetWatchAndStart();
            dapper?.Invoke();
            Utils.PrintDapper();

            if (clearData)
                _excutor.DeleteAll<Animal>();

            Utils.ResetWatchAndStart();
            excutor?.Invoke();
            Utils.PrintSqlExcutor();

            if (clearData)
                _excutor.DeleteAll<Animal>();
        }

        protected virtual void testSingleRowInsert() => doTest("single row insert",
                                                                () =>
                                                                {
                                                                    var cnn = _provider.Rent();
                                                                    _datas.ForEach(x => cnn.Execute($"insert into animal()values()"));
                                                                },
                                                                () =>
                                                                {
                                                                    _datas.ForEach(x => _excutor.Insert<Animal>(x));
                                                                },
                                                                true
                                                               );


        protected virtual void testSingleRowInsertPartial() => doTest("single row insert partial",
                                                                       () =>
                                                                       {

                                                                       },
                                                                       () =>
                                                                       {

                                                                       },
                                                                       true
                                                                     );

        protected virtual void testBulkInsert() => doTest("test bulk insert",
                                                         () =>
                                                          {

                                                          },
                                                         () =>
                                                         {

                                                         },
                                                         true
                                                        );


        protected virtual void testBulKInsertPartial() => doTest("",
                                                              () =>
                                                              {

                                                              },
                                                               () =>
                                                               {

                                                               },
                                                               true
                                                              );

        protected virtual void testDelete() => doTest("",
                                                    () =>
                                                    {

                                                    },
                                                    () =>
                                                    {

                                                    }
                                                    );

        protected virtual void testFullQuery() => doTest("",
                                                       () =>
                                                       {
                                                       },
                                                       () =>{});
        protected virtual void testFullQueryConditional() => doTest("",
                                                                  () =>
                                                                  {
                                                                  }, () =>
                                                                  {
                                                                  });
        protected virtual void testPartialQuery() => doTest("",
                                                            () => { },
                                                            () => { });

        protected virtual void testPartialQuery1() => doTest("",
                                                          () => { },
                                                          () => { });

        protected virtual void testUpdate() => doTest("",
                                                     () => { },
                                                     () => { });

        protected virtual void testUpdateBulk() => doTest("",
                                                       () => { },
                                                       () => { });


    }
}
