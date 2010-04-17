namespace Telerik.Web.Mvc.UnitTest.Data
{
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Collections;
    using System.Linq.Expressions;
    using System;
    
    using Telerik.Web.Mvc.Infrastructure;
    using Xunit;

    using Extensions;
    using Telerik.Web.Mvc.Infrastructure.Implementation;

    public class QueryableExtensionsTests
    {
        class Person
        {
            public string Name
            {
                get;
                set;
            }
            public int ID
            {
                get;
                set;
            }
        }

        [Fact]
        public void Filter_string_equals_caseinsensitive()
        {
            IEnumerable<Person> people = new[] { new Person { Name = "A" }, new Person { Name = "B" } };

            IQueryable quearyablePeople = QueryableFactory.CreateQueryable(people);

            IQueryable<Person> filteredPeople = quearyablePeople.Where(new[] { new FilterDescriptor
            {
                Member = "Name",
                Operator = FilterOperator.IsEqualTo,
                Value = "a"
            }}).Cast<Person>();

            Assert.Same(people.ElementAt(0), filteredPeople.FirstOrDefault());
        }

        [Fact]
        public void Filter_string_not_equal_caseinsensitive()
        {
            IEnumerable<Person> people = new[] { new Person { Name = "A" }, new Person { Name = "B" } };

            IQueryable quearyablePeople = QueryableFactory.CreateQueryable(people);

            IQueryable<Person> filteredPeople = quearyablePeople.Where(new[] { new FilterDescriptor
            {
                Member = "Name",
                Operator = FilterOperator.IsNotEqualTo,
                Value = "a"
            }}).Cast<Person>();

            Assert.Same(people.ElementAt(1), filteredPeople.FirstOrDefault());
        }

        [Fact]
        public void Sort_should_sort_the_data()
        {
            IEnumerable<Person> people = new[] { new Person { Name = "A" }, new Person { Name = "B" } };

            IQueryable quearyablePeople = QueryableFactory.CreateQueryable(people);

            IQueryable<Person> sortedPeople = quearyablePeople.Sort(new[] { 
                new SortDescriptor { 
                    Member = "Name", 
                    SortDirection = ListSortDirection.Descending 
                } 
            }).Cast<Person>();

            Assert.Equal("B", sortedPeople.First().Name);
        }

        [Fact]
        public void Page_should_page_the_data()
        {
            IQueryable people = CreateTestData();
            IQueryable<Person> secondPageOfPeople = people.Page(1, 5).Cast<Person>();

            Assert.Equal(5, secondPageOfPeople.First().ID);
        }

        [Fact]
        public void Filter_with_composite_descriptor_should_filter_the_data()
        {
            IQueryable people = CreateTestData();
            IQueryable<Person> filteredPeople = people.Where(new[] { 
                new CompositeFilterDescriptor {
                    FilterDescriptors = new FilterDescriptorCollection {
                           new FilterDescriptor("ID", FilterOperator.IsGreaterThanOrEqualTo, 0),
                           new FilterDescriptor("ID", FilterOperator.IsLessThanOrEqualTo, 2),
                    },
                    LogicalOperator = FilterCompositionLogicalOperator.And
                }
            }).Cast<Person>();

            Assert.Equal(3, filteredPeople.Count());
            Assert.Equal(0, filteredPeople.First().ID);
            Assert.Equal(2, filteredPeople.Last().ID);
        }

        [Fact]
        public void Filter_with_expression_should_filter_the_data()
        {
            IQueryable people = CreateTestData();
            Expression<Func<Person, bool>> expression = (Person p) => p.ID >= 0 && p.ID <= 2;
            IQueryable<Person> filteredPeople = people.Where(expression).Cast<Person>();

            Assert.Equal(0, filteredPeople.First().ID);
            Assert.Equal(2, filteredPeople.Last().ID);
        }

        [Fact]
        public void Group_with_group_descriptor_groups_the_data()
        {
            IQueryable people = CreateTestData();
            IQueryable<IGroup> grouppedPeople = people.GroupBy(new[]{new GroupDescriptor{
                        Member = "ID"
                    }
                })
                .Cast<IGroup>();

            IGroup firstGroup = grouppedPeople.First();
            IEnumerable<Person> itemsInFirstGroup = firstGroup.Items.Cast<Person>();
            Assert.Equal(0, firstGroup.Key);
            Assert.Equal(1, itemsInFirstGroup.Count());
            Assert.Equal(0, itemsInFirstGroup.First().ID);
        }

        [Fact]
        public void Empty_group()
        {
            IQueryable people = CreateTestData();
            IQueryable<Person> grouppedPeople = people.GroupBy(new GroupDescriptor[]{})
                .Cast<Person>();
            Assert.Equal(grouppedPeople.Count(), CreateTestData().Count());
        }

        private IQueryable CreateTestData()
        {
            IList<Person> people = new List<Person>();

            for (int i = 0; i < 10; i++)
            {
                people.Add(new Person { ID = i, Name = "Person#" + i });
            }

            return QueryableFactory.CreateQueryable(people);
        }
    }
}
