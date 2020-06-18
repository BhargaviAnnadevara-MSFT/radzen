using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using RadzenCrm.Data;

namespace RadzenCrm
{
    public partial class CrmService
    {
        private readonly CrmContext context;
        private readonly NavigationManager navigationManager;

        public CrmService(CrmContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public async Task ExportContactsToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/contacts/excel") : "export/crm/contacts/excel", true);
        }

        public async Task ExportContactsToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/contacts/csv") : "export/crm/contacts/csv", true);
        }

        partial void OnContactsRead(ref IQueryable<Models.Crm.Contact> items);

        public async Task<IQueryable<Models.Crm.Contact>> GetContacts(Query query = null)
        {
            var items = context.Contacts.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnContactsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactCreated(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> CreateContact(Models.Crm.Contact contact)
        {
            OnContactCreated(contact);

            context.Contacts.Add(contact);
            context.SaveChanges();

            return contact;
        }
        public async Task ExportOpportunitiesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/opportunities/excel") : "export/crm/opportunities/excel", true);
        }

        public async Task ExportOpportunitiesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/opportunities/csv") : "export/crm/opportunities/csv", true);
        }

        partial void OnOpportunitiesRead(ref IQueryable<Models.Crm.Opportunity> items);

        public async Task<IQueryable<Models.Crm.Opportunity>> GetOpportunities(Query query = null)
        {
            var items = context.Opportunities.AsQueryable();

            items = items.Include(i => i.Contact);

            items = items.Include(i => i.OpportunityStatus);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOpportunitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOpportunityCreated(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> CreateOpportunity(Models.Crm.Opportunity opportunity)
        {
            OnOpportunityCreated(opportunity);

            context.Opportunities.Add(opportunity);
            context.SaveChanges();

            return opportunity;
        }
        public async Task ExportOpportunityStatusesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/opportunitystatuses/excel") : "export/crm/opportunitystatuses/excel", true);
        }

        public async Task ExportOpportunityStatusesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/opportunitystatuses/csv") : "export/crm/opportunitystatuses/csv", true);
        }

        partial void OnOpportunityStatusesRead(ref IQueryable<Models.Crm.OpportunityStatus> items);

        public async Task<IQueryable<Models.Crm.OpportunityStatus>> GetOpportunityStatuses(Query query = null)
        {
            var items = context.OpportunityStatuses.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOpportunityStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOpportunityStatusCreated(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> CreateOpportunityStatus(Models.Crm.OpportunityStatus opportunityStatus)
        {
            OnOpportunityStatusCreated(opportunityStatus);

            context.OpportunityStatuses.Add(opportunityStatus);
            context.SaveChanges();

            return opportunityStatus;
        }
        public async Task ExportTasksToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/tasks/excel") : "export/crm/tasks/excel", true);
        }

        public async Task ExportTasksToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/tasks/csv") : "export/crm/tasks/csv", true);
        }

        partial void OnTasksRead(ref IQueryable<Models.Crm.Task> items);

        public async Task<IQueryable<Models.Crm.Task>> GetTasks(Query query = null)
        {
            var items = context.Tasks.AsQueryable();

            items = items.Include(i => i.Opportunity);

            items = items.Include(i => i.TaskType);

            items = items.Include(i => i.TaskStatus);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTasksRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTaskCreated(Models.Crm.Task item);

        public async Task<Models.Crm.Task> CreateTask(Models.Crm.Task task)
        {
            OnTaskCreated(task);

            context.Tasks.Add(task);
            context.SaveChanges();

            return task;
        }
        public async Task ExportTaskStatusesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/taskstatuses/excel") : "export/crm/taskstatuses/excel", true);
        }

        public async Task ExportTaskStatusesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/taskstatuses/csv") : "export/crm/taskstatuses/csv", true);
        }

        partial void OnTaskStatusesRead(ref IQueryable<Models.Crm.TaskStatus> items);

        public async Task<IQueryable<Models.Crm.TaskStatus>> GetTaskStatuses(Query query = null)
        {
            var items = context.TaskStatuses.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTaskStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTaskStatusCreated(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> CreateTaskStatus(Models.Crm.TaskStatus taskStatus)
        {
            OnTaskStatusCreated(taskStatus);

            context.TaskStatuses.Add(taskStatus);
            context.SaveChanges();

            return taskStatus;
        }
        public async Task ExportTaskTypesToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/tasktypes/excel") : "export/crm/tasktypes/excel", true);
        }

        public async Task ExportTaskTypesToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/crm/tasktypes/csv") : "export/crm/tasktypes/csv", true);
        }

        partial void OnTaskTypesRead(ref IQueryable<Models.Crm.TaskType> items);

        public async Task<IQueryable<Models.Crm.TaskType>> GetTaskTypes(Query query = null)
        {
            var items = context.TaskTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTaskTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTaskTypeCreated(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> CreateTaskType(Models.Crm.TaskType taskType)
        {
            OnTaskTypeCreated(taskType);

            context.TaskTypes.Add(taskType);
            context.SaveChanges();

            return taskType;
        }

        partial void OnContactDeleted(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> DeleteContact(int? id)
        {
            var item = context.Contacts
                              .Where(i => i.Id == id)
                              .Include(i => i.Opportunities)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactDeleted(item);

            context.Contacts.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnContactGet(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> GetContactById(int? id)
        {
            var items = context.Contacts
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnContactGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Crm.Contact> CancelContactChanges(Models.Crm.Contact item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnContactUpdated(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> UpdateContact(int? id, Models.Crm.Contact contact)
        {
            OnContactUpdated(contact);

            var item = context.Contacts
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(contact);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return contact;
        }

        partial void OnOpportunityDeleted(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> DeleteOpportunity(int? id)
        {
            var item = context.Opportunities
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOpportunityDeleted(item);

            context.Opportunities.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnOpportunityGet(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> GetOpportunityById(int? id)
        {
            var items = context.Opportunities
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Contact);

            items = items.Include(i => i.OpportunityStatus);

            var item = items.FirstOrDefault();

            OnOpportunityGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Crm.Opportunity> CancelOpportunityChanges(Models.Crm.Opportunity item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOpportunityUpdated(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> UpdateOpportunity(int? id, Models.Crm.Opportunity opportunity)
        {
            OnOpportunityUpdated(opportunity);

            var item = context.Opportunities
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(opportunity);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return opportunity;
        }

        partial void OnOpportunityStatusDeleted(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> DeleteOpportunityStatus(int? id)
        {
            var item = context.OpportunityStatuses
                              .Where(i => i.Id == id)
                              .Include(i => i.Opportunities)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOpportunityStatusDeleted(item);

            context.OpportunityStatuses.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnOpportunityStatusGet(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> GetOpportunityStatusById(int? id)
        {
            var items = context.OpportunityStatuses
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnOpportunityStatusGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Crm.OpportunityStatus> CancelOpportunityStatusChanges(Models.Crm.OpportunityStatus item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOpportunityStatusUpdated(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> UpdateOpportunityStatus(int? id, Models.Crm.OpportunityStatus opportunityStatus)
        {
            OnOpportunityStatusUpdated(opportunityStatus);

            var item = context.OpportunityStatuses
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(opportunityStatus);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return opportunityStatus;
        }

        partial void OnTaskDeleted(Models.Crm.Task item);

        public async Task<Models.Crm.Task> DeleteTask(int? id)
        {
            var item = context.Tasks
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTaskDeleted(item);

            context.Tasks.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnTaskGet(Models.Crm.Task item);

        public async Task<Models.Crm.Task> GetTaskById(int? id)
        {
            var items = context.Tasks
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Opportunity);

            items = items.Include(i => i.TaskType);

            items = items.Include(i => i.TaskStatus);

            var item = items.FirstOrDefault();

            OnTaskGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Crm.Task> CancelTaskChanges(Models.Crm.Task item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnTaskUpdated(Models.Crm.Task item);

        public async Task<Models.Crm.Task> UpdateTask(int? id, Models.Crm.Task task)
        {
            OnTaskUpdated(task);

            var item = context.Tasks
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(task);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return task;
        }

        partial void OnTaskStatusDeleted(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> DeleteTaskStatus(int? id)
        {
            var item = context.TaskStatuses
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTaskStatusDeleted(item);

            context.TaskStatuses.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnTaskStatusGet(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> GetTaskStatusById(int? id)
        {
            var items = context.TaskStatuses
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnTaskStatusGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Crm.TaskStatus> CancelTaskStatusChanges(Models.Crm.TaskStatus item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnTaskStatusUpdated(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> UpdateTaskStatus(int? id, Models.Crm.TaskStatus taskStatus)
        {
            OnTaskStatusUpdated(taskStatus);

            var item = context.TaskStatuses
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(taskStatus);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return taskStatus;
        }

        partial void OnTaskTypeDeleted(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> DeleteTaskType(int? id)
        {
            var item = context.TaskTypes
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTaskTypeDeleted(item);

            context.TaskTypes.Remove(item);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                context.Entry(item).State = EntityState.Unchanged;
                throw ex;
            }

            return item;
        }

        partial void OnTaskTypeGet(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> GetTaskTypeById(int? id)
        {
            var items = context.TaskTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnTaskTypeGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Crm.TaskType> CancelTaskTypeChanges(Models.Crm.TaskType item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnTaskTypeUpdated(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> UpdateTaskType(int? id, Models.Crm.TaskType taskType)
        {
            OnTaskTypeUpdated(taskType);

            var item = context.TaskTypes
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(taskType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return taskType;
        }
    }
}
