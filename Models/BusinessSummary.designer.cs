﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SME_Hub.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SmeHub")]
	public partial class BusinessSummaryDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insert_BusinessSummary(_BusinessSummary instance);
    partial void Update_BusinessSummary(_BusinessSummary instance);
    partial void Delete_BusinessSummary(_BusinessSummary instance);
    partial void Insert_PLace(_PLace instance);
    partial void Update_PLace(_PLace instance);
    partial void Delete_PLace(_PLace instance);
    #endregion
		
		public BusinessSummaryDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SmeHubConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BusinessSummaryDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BusinessSummaryDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BusinessSummaryDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BusinessSummaryDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<_BusinessSummary> _BusinessSummaries
		{
			get
			{
				return this.GetTable<_BusinessSummary>();
			}
		}
		
		public System.Data.Linq.Table<_PLace> _PLaces
		{
			get
			{
				return this.GetTable<_PLace>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[_BusinessSummary]")]
	public partial class _BusinessSummary : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _PlaceId;
		
		private string _Title;
		
		private string _Description;
		
		private EntityRef<_PLace> @__PLace;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnPlaceIdChanging(int value);
    partial void OnPlaceIdChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
		
		public _BusinessSummary()
		{
			this.@__PLace = default(EntityRef<_PLace>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlaceId", DbType="Int NOT NULL")]
		public int PlaceId
		{
			get
			{
				return this._PlaceId;
			}
			set
			{
				if ((this._PlaceId != value))
				{
					if (this.@__PLace.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPlaceIdChanging(value);
					this.SendPropertyChanging();
					this._PlaceId = value;
					this.SendPropertyChanged("PlaceId");
					this.OnPlaceIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(MAX)")]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(MAX)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="_PLace__BusinessSummary", Storage="__PLace", ThisKey="PlaceId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public _PLace _PLace
		{
			get
			{
				return this.@__PLace.Entity;
			}
			set
			{
				_PLace previousValue = this.@__PLace.Entity;
				if (((previousValue != value) 
							|| (this.@__PLace.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this.@__PLace.Entity = null;
						previousValue._BusinessSummaries.Remove(this);
					}
					this.@__PLace.Entity = value;
					if ((value != null))
					{
						value._BusinessSummaries.Add(this);
						this._PlaceId = value.Id;
					}
					else
					{
						this._PlaceId = default(int);
					}
					this.SendPropertyChanged("_PLace");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[_PLace]")]
	public partial class _PLace : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private EntitySet<_BusinessSummary> @__BusinessSummaries;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public _PLace()
		{
			this.@__BusinessSummaries = new EntitySet<_BusinessSummary>(new Action<_BusinessSummary>(this.attach__BusinessSummaries), new Action<_BusinessSummary>(this.detach__BusinessSummaries));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="_PLace__BusinessSummary", Storage="__BusinessSummaries", ThisKey="Id", OtherKey="PlaceId")]
		public EntitySet<_BusinessSummary> _BusinessSummaries
		{
			get
			{
				return this.@__BusinessSummaries;
			}
			set
			{
				this.@__BusinessSummaries.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach__BusinessSummaries(_BusinessSummary entity)
		{
			this.SendPropertyChanging();
			entity._PLace = this;
		}
		
		private void detach__BusinessSummaries(_BusinessSummary entity)
		{
			this.SendPropertyChanging();
			entity._PLace = null;
		}
	}
}
#pragma warning restore 1591