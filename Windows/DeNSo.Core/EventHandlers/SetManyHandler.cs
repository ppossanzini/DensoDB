﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using DeNSo;
using DeNSo.BSon;

namespace DeNSo.EventHandlers
{
  [HandlesCommand(DensoBuiltinCommands.SetMany)]
  [Export(typeof(ICommandHandler))]
  public class SetManyHandler : BaseCommandHandler
  {
    public override void OnHandle(IStore store,
                                  string collection,
                                  BSonDoc command,
                                  BSonDoc document)
    {
      IObjectStore st = store.GetCollection(collection);

      if (document.DocType == BSonDocumentType.BSON_DocumentArray ||
          document.DocType == BSonDocumentType.BSON_Dictionary)
      {
        var documents = document.ToList();
        if (documents != null)
          foreach (var d in documents)
            if (d is BSonDoc)
              st.Set(d as BSonDoc);

      }
      else
      {
        st.Set(document);
      }
    }
  }
}
