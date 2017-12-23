using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Persistence.Common;

namespace Persistence.LiteDB
{
    /// <summary>
    /// I decided to use RavenDB instead of SQL, to save people having to have SQL Server, and also
    /// it just takes less time to do with Raven. This is ALL the CRUD code. Simple no?
    /// 
    /// Thing is the IDatabaseAccessService and the items it persists could easily be applied to helper methods that
    /// use StoredProcedures or ADO code, the data being stored would be exactly the same. You would just need to store
    /// the individual property values in tables rather than store objects.
    /// </summary>
    public class DatabaseAccessService : IDatabaseAccessService
    {
        //EmbeddableDocumentStore documentStore = null;
		LiteDatabase _database;
        public DatabaseAccessService()
        {
			_database = new LiteDatabase("DesignerDataStore");
        }
            
        public void DeleteConnection(int connectionId)
        {
			var connections = _database.GetCollection<Connection>("Connections");
            connections.Delete(x => x.Id == connectionId);                
            
        }

        public void DeletePersistDesignerItem(int persistDesignerId)
        {
        	var designerItems = _database.GetCollection<PersistDesignerItem>("PersistDesignerItems");
        	designerItems.Delete(x => x.Id == persistDesignerId);            
        }

        public void DeleteSettingDesignerItem(int settingsDesignerItemId)
        {
			var settingsDesignerItems = _database.GetCollection<SettingsDesignerItem>("SettingsDesignerItems");
            settingsDesignerItems.Delete(x => x.Id == settingsDesignerItemId);
        }

        public int SaveDiagram(DiagramItem diagram)
        {
			var designerItems = _database.GetCollection<DiagramItem>("DiagramItems");			
			designerItems.Upsert(diagram);
			return diagram.Id;        			
        }

        public int SavePersistDesignerItem(PersistDesignerItem persistDesignerItemToSave)
        {
			var designerItems = _database.GetCollection<PersistDesignerItem>("PersistDesignerItems");
			designerItems.Upsert(persistDesignerItemToSave);
			return persistDesignerItemToSave.Id;        	        
        }

        public int SaveSettingDesignerItem(SettingsDesignerItem settingsDesignerItemToSave)
        {
			var settingsDesignerItems = _database.GetCollection<SettingsDesignerItem>("SettingsDesignerItems");
			settingsDesignerItems.Upsert(settingsDesignerItemToSave);
			return settingsDesignerItemToSave.Id;
        }

        public int SaveConnection(Connection connectionToSave)
        {
			var connections = _database.GetCollection<Connection>("Connections");
			connections.Upsert(connectionToSave);
			return connectionToSave.Id;        	
        }

        public IEnumerable<DiagramItem> FetchAllDiagram()
        {
			var diagramItems = _database.GetCollection<DiagramItem>("DiagramItems");
            return diagramItems.FindAll();
        }

        public DiagramItem FetchDiagram(int diagramId)
        {
			var diagramItems = _database.GetCollection<DiagramItem>("DiagramItems");
            return diagramItems.FindOne(x => x.Id == diagramId); 
        }

        public PersistDesignerItem FetchPersistDesignerItem(int persistDesignerItemId)
        {
			var designerItems = _database.GetCollection<PersistDesignerItem>("PersistDesignerItems");
            return designerItems.FindOne(x => x.Id == persistDesignerItemId);
        }

        public SettingsDesignerItem FetchSettingsDesignerItem(int settingsDesignerItemId)
        {
			var settingsDesignerItems = _database.GetCollection<SettingsDesignerItem>("SettingsDesignerItems");
            return settingsDesignerItems.FindOne(x => x.Id == settingsDesignerItemId);
        }

        public Connection FetchConnection(int connectionId)
        {
			var connections = _database.GetCollection<Connection>("Connections");
            return connections.FindOne(x => x.Id == connectionId);
        }

//        private int SaveItem(PersistableItemBase item)
//        {
//            using (IDocumentSession session = documentStore.OpenSession())
//            {
//                session.Store(item);
//                session.SaveChanges();
//            }
//            return item.Id;
//        }
    }
}
