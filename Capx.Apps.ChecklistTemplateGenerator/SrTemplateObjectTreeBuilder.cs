using System;
using Capx.Apps.ChecklistTemplateGenerator.DAL;
using Capx.Model.Sr.Template;

namespace Capx.Apps.ChecklistTemplateGenerator
{
    /// <summary>
    /// Helper class for template builder for add and map functions to template objects (template xsd)
    /// </summary>
    internal class SrTemplateObjectTreeBuilder
    {
        /// <summary>
        /// Adds a question to any headergroup, question, or fixedlistitem
        /// </summary>
        /// <param name="item">a headergroup, question or fixedlistitem object</param>
        /// <param name="q">question to be added</param>
        internal void AddQuestionToItem(object item, question q)
        {

            if (item.GetType() == typeof(template))
            {
                var t2 = (template)item;
                t2.templatebody = t2.templatebody.Add(q);
            }
            else if (item.GetType() == typeof(headergroup))
            {
                var h2 = (headergroup)item;
                h2.headergroupitems = h2.headergroupitems.Add(q);
            }
            else if (item.GetType() == typeof(question))
            {
                var q2 = (question)item;

                if (q2.Items == null)
                {
                    if (q2.questionfillin == false) //added rlm 10/13/2014
                    {
                        q2.Items = q2.Items.Add(new fixedlistanswer());
                        var f = (fixedlistanswer)q2.Items[0];
                        f.Items = f.Items.Add(q);
                    }
                    else { q2.Items = q2.Items.Add(q); }
                }
                else { q2.Items = q2.Items.Add(q); }
            }
            else if (item.GetType() == typeof(fixedlistitem))
            {
                var i2 = (fixedlistitem)item;

                //i2.question = i2.question.AddItem(q);
                i2.Items=i2.Items.Add(q);
            }
            else if (item.GetType() == typeof(fixedlistfillinanswer))
            {
                var a2 = (fixedlistfillinanswer)item;
                //a2.question = a2.question.AddItem(q);
                a2.Items=a2.Items.Add(q);
            }
            else if (item.GetType() == typeof(note))
            {
                var n2 = (note)item;
                n2.question = n2.question.AddItem(q);
            }
            else if (item.GetType() == typeof(fixedlistnote))
            {
                var n3 = (fixedlistnote)item;
                n3.question = n3.question.AddItem(q);
            }
        }
        /// <summary>
        /// Adds a headergroup object to any headergroup or checklist item
        /// </summary>
        /// <param name="item">a headergroup or checklist item</param>
        /// <param name="h">a headergroup object</param>
        internal void AddHeaderToItem(object item, headergroup h)
        {
            if (item.GetType() == typeof(headergroup))
            {
                var h2 = (headergroup)item;
                h2.headergroupitems = h2.headergroupitems.Add(h);
            }
            else if (item.GetType() == typeof(template))
            {
                var t = (template)item;
                t.templatebody = t.templatebody.Add(h);
            }
            else if (item.GetType() == typeof(question))
            {
                var q = (question)item;
                q.Items = q.Items.Add(h);
            }
            else if (item.GetType() == typeof(fixedlistitem))
            {
                var f = (fixedlistitem)item;
                //f.headergroup = f.headergroup.AddItem(h);
                f.Items=f.Items.Add(h);
            }
            //rlm 2015_11_23 - the following (headers under AF) is illegal in current schema
            //rlm 2015/12/4: fixed schema to support headers under Answer-fillins
            else if (item.GetType() == typeof(fixedlistfillinanswer))
            {
                var ff = (fixedlistfillinanswer)item;
                //ff.headergroup = ff.headergroup.AddItem(h);
                ff.Items=ff.Items.Add(h);
            }
        }
        /// <summary>
        /// Adds an answer to a question
        /// </summary>
        /// <param name="item">a fixedlistanswer item to hold the answer</param>
        /// <param name="a">an answer to be added</param>
        internal void AddAnswerToItem(object item, object a)
        {
            if (item.GetType() == typeof(question))
            {
                var q = (question)item;
                if (q.Items == null)
                {
                    q.Items = q.Items.Add(new fixedlistanswer());
                }
                var f = (fixedlistanswer)q.Items[0];
                f.Items = f.Items.Add(a);
            }
        }

        /// <summary>
        /// Adds a fixed list note to a question
        /// </summary>
        /// <param name="item">a question item to hold the fixed list note</param>
        /// <param name="a">a fixed list note to be added</param>
        internal void AddFixedListNoteToItem(object item, object a)
        {
            if (item.GetType() == typeof(question))
            {
                var q = (question)item;

                if (q.questionfillin == false)
                {
                    if (q.Items == null)
                    {
                        q.Items = q.Items.Add(new fixedlistanswer());
                    }
                    var f = (fixedlistanswer)q.Items[0];
                    f.Items = f.Items.Add(a);
                }

            }
        }

        /// <summary>
        /// Adds a note to an item
        /// </summary>
        /// <param name="item">a checklist, headergroup, question, or fixedlistitem</param>
        /// <param name="n">a note item to be added</param>
        internal void AddNoteToItem(object item, note n)
        {
            #region Add Note to Template
            if (item.GetType() == typeof(template))
            {
                var t = (template)item;
                t.templatebody = t.templatebody.Add(n);
            }
            else if (item.GetType() == typeof(headergroup))
            {
                var h2 = (headergroup)item;
                h2.headergroupitems = h2.headergroupitems.Add(n);
            }
            #endregion //Add Note to Template
            #region Add Note to Question

            else if (item.GetType() == typeof(question))
            {
                var q = (question)item;

                if (q.questionfillin)
                {
                    q.Items = q.Items.Add(n); //changed from q.Items.Add(n);
                }
                else if (!q.questionfillin)
                {
                    var n1 = new fixedlistnote()
                    {
                        noteid = n.noteid,
                        sortorder = n.sortorder,
                        title = n.text,
                        question = n.question
                    };

                    AddFixedListNoteToItem(item, n1);
                }
            }
            #endregion

            #region Add Note to FixedListItem
            //rlm: added 8/2/2013: It looks like AF items could not have notes as children; adding it now - this is legal in the schema
            else if (item.GetType() == typeof(fixedlistfillinanswer))   //AF
            {
                var fli = (fixedlistfillinanswer)item;
                //fli.note = fli.note.AddItem(n); //rlm: 2015-11_22 - removed
                fli.Items=fli.Items.Add(n);

                //fli.note = (note[])fli.note.Add(n); //rlm: 2015-11_22 - can only add one note child under a fli per the schema
            }

            //rlm: moved this to last, since it seems to be a default response when the Parent item is an answer choice (fixedlistitem)
            else if (item.GetType() == typeof(fixedlistitem))           //A
            {
                var fli = (fixedlistitem)item;
                //fli.note = fli.note.AddItem<note>(n); //rlm: 2015-11_22 - can only add one note child under a fli per the schema
                fli.Items=fli.Items.Add(n);
            }
            #endregion //Add Note to FixedListItem
            #region Add Note to Note
            //rlm 1/29/2013  allow a parent note (item) to have one or more child notes
            else if (item.GetType() == typeof(note))
            {
                var nItem = (note)item;
                //nItem.noteChildren = nItem.noteChildren.AddItem<note>(n);
                //rlm: 2015-11_22 - can't add note under a note per the schema
            }
            #endregion //Add Note to Note
        }

        #region Phase2 Methods

        /// <summary>
        /// Maps a DateTime value
        /// </summary>
        /// <param name="value">value to be mapped</param>
        /// <returns>return datetime if value is not DBNull or else returns null</returns>
        internal DateTime? MapDBNullToDT(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            return Convert.ToDateTime(value);
        }

        /// <summary>
        /// Creates a note object from parameters
        /// </summary>
        /// <returns>a new note object</returns>
        internal note MapNote(
            //!Base item metadata
            String type,
            String styleClass,
            String shortName,

            //!Displayed items: Q, S, N (DI), A (LI)
            Decimal itemCkey,
            String visibleText,
            String longText,
            String reportText,

            //!All Displayed Items
            Boolean enabled,
            Boolean visible,
            String tooltip,
            String popupText,
            String linkText,
            String linkText2,
            String Source,  //TODO: not in the Question object //Source is AuthorityID 
            Boolean showInReport,
            Boolean mustImplement,
            Int32 sortorder,

            //!Sections, Questions, Notes
            Boolean AuthorityRequired //ordinarily, this "required" flag only applies to Questions and the Sections (eCC headers) that contain them
            )
        {
            var n = new note();
            n.noteid = itemCkey;

            //Don't serialize the follwing attributes if they are empty; use if clause to ensure they are not touched if they have default values
            //Need to check if this keep them out of the serialized XML.  If it has no effect, then most of the if clauses can be removed. 
            //This is an attempt to avoid cluttering the XML with default-valued attributes.

            //!Base item metadata
            if (type != String.Empty) n.type = type;
            if (styleClass != String.Empty) n.styleClass = styleClass;
            //if (shortName != String.Empty) n.name = shortName;
            //!All Displayed Items
            n.text = visibleText;
            //if (longText != string.Empty) n.alttext = longText;  TODO: add altText/longText to header in Schema? (and TE)
            if (reportText != string.Empty)
            {
                n.reportText = reportText;
                if (n.reportText == "''") n.reportText = "{No text}";
                if (n.reportText.StartsWith("]"))
                    n.reportText = n.reportText.TrimStart(new char[] { ']' });
            }

            n.enabled = enabled; //show only when false
            n.visible = visible;
            if (tooltip != String.Empty) n.tooltip = tooltip;
            if (popupText != String.Empty) n.popupText = popupText;
            if (linkText != String.Empty) n.linkText = linkText;
            if (linkText2 != String.Empty) n.linkText2 = linkText2;
            //n.Source = Source; 
            //TODO: Why isn't Source in Question in the Schema?  Is it a coding construct?  Should it be treated as OtherText (note)?

            //if (n.text.Contains("Reporting Note"))
            //{
            //    n.visible = false;
            //    //n.showInReport = true;

            //    n.reportText = n.text.Replace("#", "").Replace("Reporting Note: ", "").Trim();
            //    n.text = "";
            //}
            //else if (string.IsNullOrEmpty(n.reportText)) n.reportText = "{No text}";   //n.showInReport = false;  //showInReport can't be false if the note has children

            if (mustImplement == false) n.mustImplement = mustImplement;  //default is true; //eCC has no concept of optionality of Notes at the current time.  If it is added, it will be set explicity in the Template Editor

            //if (!showInReport) n.showInReport = false;
            //if (mustImplement == false) n.mustImplement = mustImplement;  //default is true

            ////!Handle Conditional Reporting with "?"
            //if (n.text.StartsWith("?"))
            //{
            //    n.mustImplement = true;
            //    n.text.TrimStart('?');
            //}

            if (sortorder >= 0) n.sortorder = sortorder;

            //!Special handling for Authority Required
            if (AuthorityRequired) n.mustImplement = true;
            else n.mustImplement = false;

            //Since almost all note in TE are authorityRequired="false", and we need this attribute to set mustImplement
            //We need a kludge that sets all notes to mustImplement = "true"  This is temporary, and it could possibly mess up the status of notes with children.
            n.mustImplement = true;

            return n;

        }



        /// <summary>
        /// Creates a fixed list note object from parameters
        /// </summary>
        /// <param name="type">@type</param>
        /// <param name="styleClass">@styleClass</param>
        /// <param name="shortName">@name</param>
        /// <param name="itemCkey">@id</param>
        /// <param name="visibleText"></param>
        /// <param name="longText">@alt-text; OtherText</param>
        /// <param name="reportText">@reportText, OtherText</param>
        /// <param name="enabled">@enabled</param>
        /// <param name="visible">@visible</param>
        /// <param name="tooltip">@tooltip; OtherText</param>
        /// <param name="popupText">@popupText; OtherText</param>
        /// <param name="linkText">@linkText; Link</param>
        /// <param name="linkText2">@linkText2</param>
        /// <param name="Source">@Code</param>
        /// <param name="showInReport">@showInReport</param>
        /// <param name="mustImplement">@mustImplement</param>
        /// <param name="sortorder">@sort-order, @order</param>
        /// <param name="AuthorityRequired">Required; @minCard ="1" (or more)</param>
        /// <returns>New fixed list note object</returns>
        internal fixedlistnote MapFixedListNote(
            //!Base item metadata
            String type,
            String styleClass,
            String shortName,

            //!Displayed items: Q, S, N (DI), A (LI)
            Decimal itemCkey,
            String visibleText,
            String longText,
            String reportText,

            //!All Displayed Items
            Boolean enabled,
            Boolean visible,
            String tooltip,
            String popupText,
            String linkText,
            String linkText2,
            String Source,  //TODO: not in the Question object //Source is AuthorityID 
            Boolean showInReport,
            Boolean mustImplement,
            Int32 sortorder,

            //!Sections, Questions, Notes
            Boolean AuthorityRequired //ordinarily, this "required" flag only applies to Questions and the Sections (eCC headers) that contain them
)
        {
            //identical to note
            var n = new fixedlistnote();
            n.noteid = itemCkey;

            //Don't serialize the follwing attributes if they are empty; use if clause to ensure they are not touched if they have default values
            //Need to check if this keep them out of the serialized XML.  If it has no effect, then most of the if clauses can be removed. 
            //This is an attempt to avoid cluttering the XML with default-valued attributes.

            //!Base item metadata
            if (type != String.Empty) n.type = type;
            if (styleClass != String.Empty) n.styleClass = styleClass;
            //if (shortName != String.Empty) n.name = shortName;
            //!All Displayed Items
            n.title = visibleText;
            //if (longText != string.Empty) n.alttext = longText;  TODO: add altText/longText to header in Schema? (and TE)
            if (reportText != string.Empty)
            {
                n.reportText = reportText;
                if (n.reportText == "''") n.reportText = "{No text}";
                if (n.reportText.StartsWith("]"))
                    n.reportText = n.reportText.TrimStart(new char[] { ']' });
            }

            n.enabled = enabled; //show only when false
            n.visible = visible;
            if (tooltip != String.Empty) n.tooltip = tooltip;
            if (popupText != String.Empty) n.popupText = popupText;
            if (linkText != String.Empty) n.linkText = linkText;
            if (linkText2 != String.Empty) n.linkText2 = linkText2;
            //n.Source = Source; 
            //TODO: Why isn't Source in Question in the Schema?  Is it a coding construct?  Should it be treated as OtherText (note)?

            //if (!showInReport) n.showInReport = false;
            //if (n.title.StartsWith("# Reporting Note"))
            //{
            //    n.visible = false;
            //    n.showInReport = true;
            //    n.title = "";
            //}
            //else n.reportText = "{No text}"; //n.showInReport = false;

            if (mustImplement == false) n.mustImplement = mustImplement;  //default is true

            ////!Handle Conditional Reporting with "?"
            //if (n.text.StartsWith("?"))
            //{
            //    n.mustImplement = true;
            //    n.title.TrimStart('?');
            //}

            if (sortorder >= 0) n.sortorder = sortorder;

            //!Special handling for Authority Required
            if (AuthorityRequired) n.mustImplement = true;
            //else n.mustImplement = false; //eCC has no concept of optionality of ListNotes at the current time.  If it is added, it will be set explicity in the Template Editor

            //Since almost all note in TE are authorityRequired="false", and we need this attribute to set mustImplement
            //We need a kludge that sets all notes to mustImplement = "true"  This is temporary, and it could possibly mess up the status of notes with children.
            n.mustImplement = true;

            return n;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">aaa</param>
        /// <param name="styleClass">aaa</param>
        /// <param name="shortName">aaa</param>
        /// <param name="itemCkey">aaa</param>
        /// <param name="visibleText">aaa</param>
        /// <param name="longText">aaa</param>
        /// <param name="reportText">aaa</param>
        /// <param name="enabled">aaa</param>
        /// <param name="visible">aaa</param>
        /// <param name="tooltip">aaa</param>
        /// <param name="popupText">aaa</param>
        /// <param name="linkText">aaa</param>
        /// <param name="linkText2">aaa</param>
        /// <param name="Source">aaa</param>
        /// <param name="showInReport">aaa</param>
        /// <param name="mustImplement">aaa</param>
        /// <param name="sortorder">aaa</param>
        /// <param name="qType">aaa</param>
        /// <param name="dataType">aaa</param>
        /// <param name="answerUnits">aaa</param>
        /// <param name="textAfterResponse">aaa</param>
        /// <param name="colTextDelimiter">aaa</param>
        /// <param name="numCols">aaa</param>
        /// <param name="storedCol">aaa</param>
        /// <param name="listHeaderText">aaa</param>
        /// <param name="minSelections">aaa</param>
        /// <param name="maxSelections">aaa</param>
        /// <param name="locked">aaa</param>
        /// <param name="minCard">aaa</param>
        /// <param name="maxCard">aaa</param>
        /// <param name="AuthorityRequired">aaa</param>
        /// <returns>aaa</returns>
        internal question MapQuestion(
            //!Base item metadata
            String type,
            String styleClass,
            String shortName,

            //!Displayed items: Q, S, N (DI), A (LI)
            Decimal itemCkey,
            String visibleText,
            String longText,
            String reportText,

            //!All Displayed Items
            Boolean enabled,
            Boolean visible,
            String tooltip,
            String popupText,
            String linkText,
            String linkText2,
            String Source,  //TODO: not in the Question object //Source is AuthorityID 
            Boolean showInReport,
            Boolean mustImplement,
            Int32 sortorder,

            //!Questions: QF, QM or QS
            ItemType qType,

            //!Response items QF and AF
            String dataType,
            String answerUnits,
            String textAfterResponse,

            //!AF only
            //Boolean responseRequired;  //not used here

            //!List Items
            String colTextDelimiter,
            Int16 numCols,
            Int16 storedCol,
            String listHeaderText,
            Int16 minSelections,
            Int16 maxSelections,

            //!Sections and Questions are repeatable items
            Boolean locked,
            Int32 minCard,
            Int32 maxCard,
            Boolean AuthorityRequired //ordinarily, this "required" flag only applies to Questions and the Sections (eCC headers) that contain them

            )
        {
            var q = new question();
            //Don't serialize the follwing attributes if they are empty; use if clause to ensure they are not touched if they have default values
            //Need to check if this keep them out of the serialized XML.  If it has no effect, then most of the if clauses can be removed. 
            //This is an attempt to avoid cluttering the XML with default-valued attributes.

            //!Base item metadata
            if (type != String.Empty) q.type = type;
            if (styleClass != String.Empty) q.styleClass = styleClass;
            //if (shortName != String.Empty) q.name = shortName;
            //!All Displayed Items
            q.questionid = itemCkey;

            q.title = visibleText;
            if (longText != string.Empty) q.alttext = longText;
            if (reportText != string.Empty)
            {
                q.reportText = reportText;
                if (q.reportText == "''") q.reportText = "{No text}";
                if (q.reportText.StartsWith("]"))
                    q.reportText = q.reportText.TrimStart(new char[] { ']' });
            }

            q.enabled = enabled; //show only when false
            q.visible = visible;
            if (tooltip != String.Empty) q.tooltip = tooltip;
            if (popupText != String.Empty) q.popupText = popupText;
            if (linkText != String.Empty) q.linkText = linkText;
            if (linkText2 != String.Empty) q.linkText2 = linkText2;
            //q.Source = Source; 
            //TODO: Why isn't Source in Question in the Schema?  Is it a coding construct?  Should it be treated as OtherText (note)?

            if (!showInReport) q.showInReport = false;
            if (mustImplement == false) q.mustImplement = mustImplement;  //default is true


            if (sortorder >= 0) q.sortorder = sortorder;

            //!Sections and Questions are repeatable items//
            if (locked) q.locked = true;  //show only when true
            q.minCard = (UInt16)minCard; //default is 1 (meaning that the Section or Question is required)
            //if (maxCard == 0) q.maxCard = 1; //TODO: need to update database to set default maxRepetitions to 1
            //if (maxCard > 1) 
            q.maxCard = (UInt16)maxCard;
            //Console.WriteLine( q.title, q.maxCard.ToString(), maxCard.ToString());

            //!Special handling for Authority Required
            if (AuthorityRequired)
            {
                q.required = true;
                q.minCard = 1;
                q.mustImplement = true;
            }
            else
            {
                q.required = false;
                //q.minCard = 0;  removed 6/30/17
                //q.mustImplement = false;  removed 6/30/17
            }

            //!Handle Conditional Reporting with "?"
            if (q.title.StartsWith("?"))
            {
                q.mustImplement = true;
                //q.title = q.title.TrimStart('?');
            }

            //!Questions: QF, QM or QS
            if (qType == ItemType.QUESTIONFILLIN)
            {
                q.questionfillin = true;
                if (dataType != string.Empty) q.datatype = dataType;
                if (answerUnits != string.Empty) q.answerunits = answerUnits;
                if (textAfterResponse != string.Empty) q.textAfterResponse = textAfterResponse;

                //!Handle optional Comments
                if (q.title.ToLower().StartsWith("comment(s)"))
                    q.mustImplement = true;

            }

            //!List Items
            if (qType == ItemType.QUESTIONMULTIPLE || qType == ItemType.QUESTIONSINGLE)
            {//add a wrapper element (fixedlistanswer) for all the list items that will be added later
                var f = new fixedlistanswer();

                if (numCols > 0)
                {
                    f.numCols = (byte)numCols;
                    f.colTextDelimiter = colTextDelimiter.Trim();
                    f.storedCol = (storedCol == (byte)0) ? (byte)1 : (byte)storedCol;
                    if (listHeaderText != String.Empty) f.listHeaderText = listHeaderText;
                }

                //!QM
                if (qType == ItemType.QUESTIONMULTIPLE)
                {
                    f.allowmultipleselection = (qType == ItemType.QUESTIONMULTIPLE); //just trying out a Boolean trick :-)
                    if (minSelections > 1) f.minSelections = (UInt16)minSelections;
                    if (maxSelections > 0) f.maxSelections = (UInt16)maxSelections;
                }
                q.Items = q.Items.Add(f);
            }

            return q;

        }

        /// <summary>
        /// Creates a new question authority required object with input parameters
        /// </summary>
        /// <param name="authorityId">authority id (Source from ListOfSources for item)</param>
        /// <returns>a new questionauthorityrequired object</returns>
        internal questionAuthorityrequired MapAuthorityRequired(String authorityId)
        {
            var a = new questionAuthorityrequired();
            a.authorityid = new String[1];
            a.authorityid[0] = authorityId;
            return a;
        }

        /// <summary>
        /// Creates a new proceduresProcedure object with input parameters
        /// </summary>
        /// <param name="title">fully qualified name of the procedure record</param>
        /// <returns>a new proceduresProcedure object</returns>
        internal proceduresProcedure MapProcedure(String title)
        {
            var a = new proceduresProcedure();
            a.title = title;
            return a;
        }

        /// <summary>
        /// Creates a version object from input parameters
        /// </summary>
        /// <param name="displayName">name of the versioning orginalization (CS, AJCC, FIGO)</param>
        /// <param name="majorVersion">major version</param>
        /// <param name="minorVersion">minor version</param>
        /// <param name="SchemaNum">schema number of version</param>
        /// <returns>a new version object</returns>
        internal version MapVersion(String displayName, String majorVersion, String minorVersion, String SchemaNum)
        {
            var a = new version();
            a.displayname = displayName;
            a.majorversion = majorVersion;
            a.minorversion = minorVersion;
            //a.schemaname = SchemaNum;
            return a;
        }

        /// <summary>
        /// Creates a headergroup object from input parameters
        /// </summary>
        /// <param name="itemCkey">headergroup id</param>
        /// <param name="visibleText">title</param>
        /// <param name="reportText">sdcReportText attribute</param>
        /// <param name="sortorder">sort order</param>
        /// <param name="AuthorityRequired">authority required</param>
        /// <param name="maxCard">...</param>
        /// <param name="minCard">...</param>
        /// <returns>a new headergroup object</returns>
        internal headergroup MapHeaderGroup(
            //!Base item metadata
            String type,
            String styleClass,
            String shortName,

            //!Displayed items: Q, S, N (DI), A (LI)
            Decimal itemCkey,
            String visibleText,
            String longText,
            String reportText,

            //!All Displayed Items
            Boolean enabled,
            Boolean visible,
            String tooltip,
            String popupText,
            String linkText,
            String linkText2,
            String Source,  //TODO: not in the Question object //Source is AuthorityID 
            Boolean showInReport,
            Boolean mustImplement,
            Int32 sortorder,

            //!Sections and Questions are repeatable items
            Boolean locked,
            Int32 minCard,
            Int32 maxCard,

            //!Sections, Questions, Notes
            Boolean AuthorityRequired //ordinarily, this "required" flag only applies to Questions and the Sections (eCC headers) that contain them

            )
        {
            var h = new headergroup();

            //Don't serialize the follwing attributes if they are empty; use if clause to ensure they are not touched if they have default values
            //Need to check if this keep them out of the serialized XML.  If it has no effect, then most of the if clauses can be removed. 
            //This is an attempt to avoid cluttering the XML with default-valued attributes.

            //!Base item metadata
            if (type != String.Empty) h.type = type;
            if (styleClass != String.Empty) h.styleClass = styleClass;
            //if (shortName != String.Empty) h.name = shortName;
            //!All Displayed Items
            h.headergroupid = itemCkey;
            h.title = visibleText;
            //if (longText != string.Empty) h.alttext = longText;  TODO: add altText/longText to header in Schema? (and TE)
            if (reportText != string.Empty)
            {
                h.reportText = reportText;
                if (h.reportText == "''") h.reportText = "{No text}";
                if (h.reportText.StartsWith("]"))
                    h.reportText = h.reportText.TrimStart(new char[] { ']' });
            }

            h.enabled = enabled; //show only when false
            h.visible = visible;
            if (tooltip != String.Empty) h.tooltip = tooltip;
            if (popupText != String.Empty) h.popupText = popupText;
            if (linkText != String.Empty) h.linkText = linkText;
            if (linkText2 != String.Empty) h.linkText2 = linkText2;
            //h.Source = Source; 
            //TODO: Why isn't Source in Question in the Schema?  Is it a coding construct?  Should it be treated as OtherText (note)?

            if (!showInReport) h.showInReport = false;
            if (mustImplement == false) h.mustImplement = mustImplement;  //default is true

            //!Handle Conditional Reporting with "?"
            if (h.title.StartsWith("?"))
            {
                h.mustImplement = true;
                h.title.TrimStart('?');
            }

            if (sortorder >= 0) h.sortorder = sortorder;

            //!Sections and Questions are repeatable items//
            if (locked) h.locked = true;  //show only when true
            h.minCard = (UInt16)minCard; //default is 1 (meaning that the Sectin or Question is required)
            if (maxCard == 0) h.maxCard = 1; //TODO: need to update database to set default maxRepetitions to 1
            if (maxCard > 1) h.maxCard = (UInt16)maxCard;


            //!Special handling for Authority Required
            if (AuthorityRequired)
            {
                h.required = true;
                h.minCard = 1;
                h.mustImplement = true;
            }
            else
            {
                h.required = false;
                //h.minCard = 0; removed 6/30/17
                //h.mustImplement = false; removed 6/30/17
            }

            return h;
        }
   
            #region old header stuff
            //h.title = visibleText;
            //if (reportText != string.Empty) { h.reportText = reportText; }
            //h.sortorder = sortorder;
            ////h.required = required;

            //h.minCard = (ushort)minCard;
            //h.maxCard = (ushort)maxCard;


            ////rlm: 2015_12_12  changes for SDC compatibility
            //if (AuthorityRequired)
            //{
            //    h.required = true;
            //    h.minCard = 1;
            //    h.mustImplement = true;
            //}
            //else
            //{
            //    h.required = false;
            //    h.minCard = 0;
            //    h.mustImplement = false;
            //}
            #endregion


            
             
        

        /// <summary>
        /// Creates a fixedlistitem (an answer item) with the input parameters
        /// </summary>
        /// <param name="itemCkey">answer id</param>
        /// <param name="visibleText">title</param>
        /// <param name="reportText">sdcReportText attribute</param>
        /// <param name="sortorder">sort order</param>
        /// <param name="sdc">selection-disables-children</param>
        /// <param name="sds">selection-deselects-siblings</param>
        /// <param name="omitWhenSelected">...</param>
        /// <returns>a new fixedlistitem object</returns>
        internal fixedlistitem MapAnswer(
           //!Base item metadata
            String type,
            String styleClass,
            String shortName,            
            
            //!Displayed items: Q, S, N (DI), A (LI)
            Decimal itemCkey,
            String visibleText,
            String longText,
            String reportText,

            //!All Displayed Items
            Boolean enabled,
            Boolean visible,
            String tooltip,
            String popupText,
            String linkText,
            String linkText2,
            String Source,  //TODO: not in the Question object //Source is AuthorityID 
            Boolean showInReport,
            Boolean mustImplement,
            Int32   sortorder,

            //!List Items (A, AF)
            Boolean sdc,
            Boolean sds,
            Boolean omitWhenSelected

            //!Response items QF and AF
            //String dataType,
            //String answerUnits,
            //String textAfterResponse,

            //!AF only
            //Boolean responseRequired  //not used here

            )
        {
            var a = new fixedlistitem();

            //Don't serialize the follwing attributes if they are empty; use if clause to ensure they are not touched if they have default values
            //Need to check if this keep them out of the serialized XML.  If it has no effect, then most of the if clauses can be removed. 
            //This is an attempt to avoid cluttering the XML with default-valued attributes.

            //!Base item metadata
            if (type != String.Empty) a.type = type;
            if (styleClass != String.Empty) a.styleClass = styleClass;
            //if (shortName != String.Empty) a.name = shortName;
            //!All Displayed Items
            a.answerid = itemCkey;
            a.title = visibleText;
            //TODO: if (longText != string.Empty) a.alttext = longText;
            if (reportText != string.Empty)
            {
                a.reportText = reportText;
                if (a.reportText == "''") a.reportText = "{No text}";
                if (a.reportText.StartsWith("]"))
                    a.reportText = a.reportText.TrimStart(new char[] { ']' });
            }

            a.enabled = enabled; //show only when false
            a.visible = visible;
            if (tooltip != String.Empty) a.tooltip = tooltip;
            if (popupText != String.Empty) a.popupText = popupText;
            if (linkText != String.Empty) a.linkText = linkText;
            if (linkText2 != String.Empty) a.linkText2 = linkText2;
            //a.Source = Source; 
            //TODO: Why isn't Source in Question in the Schema?  Is it a coding construct?  Should it be treated as OtherText (note)?

            if (!showInReport) a.showInReport = false;
            //if (mustImplement == false) a.mustImplement = mustImplement;  //default is true

            //!Handle Conditional Reporting with "?"
            if (a.title.StartsWith("?"))
            {
                a.mustImplement = true;
                //a.title=a.title.TrimStart('?');
                a.omitWhenSelected = true;
            }
            //!Handle "Optional" Answers with "+"
            if (a.title.StartsWith("+"))
            {
                a.mustImplement = false;
                //a.title = a.title.TrimStart('+');
            }

            if (sortorder >= 0) a.sortorder = sortorder;

            //!List Items Only
            a.selectiondisableschildren = sdc;
            a.selectiondeselectssiblings = sds;
            //a.omitWhenSelected = omitWhenSelected;

            return a;
        }

        /// <summary>
        /// Creates a displayProperty (for an answer item only) with the input parameters
        /// </summary>
        /// <param name="displayName">combocol name</param>
        /// <param name="displayValue">combocol value</param>
        /// <returns></returns>
        internal displayProperty MapDisplayProperty(String displayName, String displayValue)
        {
            var a = new displayProperty();
            a.name = displayValue;
            a.value = displayValue;
            return a;
        }

        /// <summary>
        /// Creates a fixedlistfillinanswer object (an answer item)
        /// </summary>
        /// <param name="itemCkey">answer id</param>
        /// <param name="visibleText">title</param>
        /// <param name="reportText">sdcReportText attribute</param>
        /// <param name="sortorder">sort order</param>
        /// <param name="dataType">dataType</param>
        /// <param name="answerUnits">answerUnits</param>
        /// <param name="sdc">selection-disables-children</param>
        /// <param name="sds">selection-deselects-siblings</param>
        /// <param name="omitWhenSelected">...</param>
        /// <param name="responseRequired">...</param>
        /// <param name="textAfterResponse">...</param>
        /// <returns>a new fixedlistfillinanswer object with length default to 200</returns>
        internal fixedlistfillinanswer MapAnswerFillin(
            //!Base item metadata
            String type,
            String styleClass,
            String shortName,

            //!Displayed items: Q, S, N (DI), A (LI)
            Decimal itemCkey,
            String visibleText,
            String longText,
            String reportText,

            //!All Displayed Items
            Boolean enabled,
            Boolean visible,
            String tooltip,
            String popupText,
            String linkText,
            String linkText2,
            String Source,  //TODO: not in the Question object //Source is AuthorityID 
            Boolean showInReport,
            Boolean mustImplement,
            Int32 sortorder,

            //!List Items (A, AF)
            Boolean sdc,
            Boolean sds,
            Boolean omitWhenSelected,

            //!Response items QF and AF
            String dataType,
            String answerUnits,
            String textAfterResponse,

            //!AF only
            Boolean responseRequired  //not used here
            )
        {
            var af = new fixedlistfillinanswer();
            af.answerid = itemCkey;

            //Don't serialize the follwing attributes if they are empty; use if clause to ensure they are not touched if they have default values
            //Need to check if this keep them out of the serialized XML.  If it has no effect, then most of the if clauses can be removed. 
            //This is an attempt to avoid cluttering the XML with default-valued attributes.

            //!Base item metadata
            if (type != String.Empty) af.type = type;
            if (styleClass != String.Empty) af.styleClass = styleClass;
            //if (shortName != String.Empty) af.name = shortName;

            //!All Displayed Items
            af.answerid = itemCkey;
            af.title = visibleText;
            //TODO: if (longText != string.Empty) af.alttext = longText;
            if (reportText != string.Empty)
            {
                af.reportText = reportText;
                if (af.reportText == "''") af.reportText = "{No text}";
                if (af.reportText.StartsWith("]"))
                    af.reportText = af.reportText.TrimStart(new char[] { ']' });
            }

            af.enabled = enabled; //show only when false
            af.visible = visible;
            if (tooltip != String.Empty) af.tooltip = tooltip;
            if (popupText != String.Empty) af.popupText = popupText;
            if (linkText != String.Empty) af.linkText = linkText;
            if (linkText2 != String.Empty) af.linkText2 = linkText2;
            //af.Source = Source; 
            //TODO: Why isn't Source in Question in the Schema?  Is it a coding construct?  Should it be treated as OtherText (note)?

            if (!showInReport) af.showInReport = false;
            //if (mustImplement == false) af.mustImplement = mustImplement;  //default is true

            //!Handle Conditional Reporting with "?"
            if (af.title.StartsWith("?"))
            {
                af.mustImplement = true;
                //af.title=af.title.TrimStart('?');
                af.omitWhenSelected = true;
            }
            //!Handle "Optional" Answers with "+"
            if (af.title.StartsWith("+"))
            {
                af.mustImplement = false;
                //af.title = af.title.TrimStart('+');
            }

            if (sortorder >= 0) af.sortorder = sortorder;

            //!List Items Only
            af.selectiondisableschildren = sdc;
            af.selectiondeselectssiblings = sds;
            //af.omitWhenSelected = omitWhenSelected;

            //!AF and QF Only
            if (dataType != string.Empty) { af.datatype = dataType; }
            if (answerUnits != string.Empty) { af.answerunits = answerUnits; }



            if (textAfterResponse != String.Empty) af.textAfterResponse = textAfterResponse;
            af.responseRequired = responseRequired;

            if (af.title.ToLower().Contains("specify") ||
                af.title.ToLower().Contains("explain") ||
                af.title.ToLower().Contains("at least") ||
                af.title.ToLower().Contains("(mm)") ||
                af.title.ToLower().Contains("(cm)"))
            {
                af.responseRequired = true;
            }
            else
            {
                af.responseRequired = false;
            }
            if (responseRequired == true) af.responseRequired = true;

            return af;

            
        }


        /// <summary>
        /// Creates a new checklist object
        /// </summary>
        /// <param name="versionCkey">template id and version ckey</param>
        /// <param name="checklistCkey">ckey</param>
        /// <param name="required"></param>
        /// <param name="templatexmlversion"> version of xml file </param>
        /// <param name="fileName">the file name for the XML instance document</param>
        /// <returns>a new checklist object</returns>
        internal template MapTemplate(
            Decimal versionCkey,
            String checklistCkey,
            Boolean required,
            String templatexmlversion,
            String fileName,
            String releaseVersionSuffix)
        {
            var a = new template();
            a.templateid = versionCkey; //TODO: rlm: duplicate of a.versionckey 
            a.checklistid = checklistCkey;
            a.required = required;
            a.templatexmlversion = templatexmlversion;
            a.fileName = fileName.TrimEnd() + (releaseVersionSuffix ?? "") + "_enh.xml"; //rlm: added 2105_12_10; "enh" means "enhanced eCC"; 12/31/2017: added releaseVersionSuffix
            a.schemaversion = "2015_Dec_12"; //rlm: added 2015_Dec_12

            return a;
        }

        /// <summary>
        /// Creates a new Template header object
        /// </summary>
        /// <param name="title">category of checklist header</param>
        /// <param name="restrictions">restrictions of checklist header</param>
        /// <param name="category">category of checklist</param>
        /// <param name="genericHeader">generic header of checklist</param>
        /// <returns>a new template header object</returns>
        internal templateheader MapTemplateHeader(String title, String restrictions, String category, String genericHeader)
        {
            var a = new templateheader();
            a.title = title.Replace("College of American Pathologists Cancer Checklist;", "").Trim();
            a.category = category;
            a.restrictions = restrictions;
            a.genericheader = genericHeader;
            return a;
        }

        /// <summary>
        /// Creates a new checklist header publication object
        /// </summary>
        /// <param name="webPostingDate">the web posting date of publication</param>
        /// <param name="revisionDate">the revision date of publication</param>
        /// <param name="effectiveDate">the effective date of publication</param>
        /// <param name="retireDate">the retire date of publication</param>
        /// <param name="approvalStatus">the approval status of publication</param>
        /// <returns>a new checklist header publication</returns>
        internal templateheaderPublication MapPublication(DateTime? webPostingDate, DateTime? revisionDate, DateTime? effectiveDate,
            DateTime? retireDate, String approvalStatus)
        {
            var a = new templateheaderPublication();
            a.approvalstatus = approvalStatus;
            if (webPostingDate != null)
            {
                a.webpostingdate = webPostingDate.Value;
                a.webpostingdateSpecified = true;
            }
            if (revisionDate != null)
            {
                a.revisiondate = revisionDate.Value;
                a.revisiondateSpecified = true;
            }
            if (effectiveDate != null)
            {
                a.effectivedate = effectiveDate.Value;
                a.effectivedateSpecified = true;
            }
            if (retireDate != null)
            {
                a.retirementdate = retireDate.Value;
                a.retirementdateSpecified = true;
            }
            return a;
        }

        #endregion
    }
}