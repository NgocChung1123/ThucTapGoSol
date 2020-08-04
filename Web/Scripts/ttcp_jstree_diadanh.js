/**
 * @author trungnm
 */

var tree_id = "#treeview";
var show_edit_box = false;
var path_to_icon = "";

//focus to the tree
function focusTree()
{
	$(tree_id).jstree("set_focus");
}

// refresh the whole tree
function refreshTree()
{
	$(tree_id).jstree("refresh",-1);
}

// add root node
function addNewRootNode()
{
    //alert("hĩhix");
	var currentTree = $.jstree._focused();
	var new_node_string = currentTree._get_string("new_node");
	$(tree_id).jstree("create",-1,false,new_node_string,false,false);
}

// search
function search_tree()
{
	clear_search();
	var search_term = $.trim($("#search_text").val());
    if (search_term != "")
    {
    	//$.blockUI({ 
		//	css: { 
	    //        border: 'none', 
	    //        padding: '15px', 
	    //        backgroundColor: '#000', 
	    //        '-webkit-border-radius': '10px', 
	    //        '-moz-border-radius': '10px', 
	    //        opacity: .5, 
	    //        color: '#fff' 
        //	} 
    	//});
    	$(tree_id).jstree("search", search_term);
    	
    }
}

// clear the previous search and close all nodes before do the new search
function clear_search()
{
	$(tree_id).jstree("close_all", -1, true);
	$(tree_id).jstree("clear_search");
}

// init tree with given data
function init_tree(tree_data) {
	show_edit_box = tree_data.show_edit_box;
	$.jstree._themes = tree_data.theme_path;
	path_to_icon = tree_data.theme_path + tree_data.theme + "/";
	tree_id = tree_data.tree_id;
	delete_deny = tree_data.delete_deny;
	create_deny = tree_data.create_deny;
	edit_deny = tree_data.edit_deny;

	$(tree_id)
	// call `.jstree` with the options object
		.jstree({
		    // the `plugins` array allows you to configure the active plugins on this instance
		    "plugins": ["themes", "json_data", "search", "ui", "crrm", "types", "hotkeys", "sort", "contextmenu"],

		    // types
		    "types": {
		        // I set both options to -2, as I do not need depth and children count checking
		        // Those two checks may slow jstree a lot, so use only when needed
		        "max_depth": -2,
		        "max_children": -2,
		        // This will prevent moving or creating any other type as a root node
		        "types": {
		            "default": {
		                "valid_children": ["default"]
		            },
		            // The `tỉnh` type
		            "tỉnh": {
		                //"valid_children" : [ "huyện", "thị xã", "quận", "thành phố", "khác" ],
		                "icon": {
		                    "image": path_to_icon + "tinh.png"
		                }
		            },
		            "thành phố": {
		                "icon": {
		                    "image": path_to_icon + "thanhpho.png"
		                }
		            },
		            "huyện": {
		                "icon": {
		                    "image": path_to_icon + "huyen.png"
		                }
		            },
		            "quận": {
		                "icon": {
		                    "image": path_to_icon + "quan.png"
		                }
		            },
		            "thị xã": {
		                "icon": {
		                    "image": path_to_icon + "thixa.png"
		                }
		            },
		            "xã": {
		                "icon": {
		                    "image": path_to_icon + "xa.png"
		                }
		            },
		            "phường": {
		                "icon": {
		                    "image": path_to_icon + "phuong.png"
		                }
		            },
		            "thị trấn": {
		                "icon": {
		                    "image": path_to_icon + "thitran.png"
		                }
		            },
		            "khác": {
		                "icon": {
		                    "image": path_to_icon + "khac.png"
		                }
		            }
		        }
		    },

		    //search
		    "search": {
		        // As this has been a common question - async search
		        // Same as above - the `ajax` config option is actually jQuery's AJAX object
		        "ajax": {
		            "type": "POST",
		            "url": tree_data.search_url,
		            // You get the search string as a parameter
		            "data": function (str) {
		                //return {
		                //    "operation": "search",
		                //    "q": str
		                //};
		                ////alert(1);
		                ////str = $.trim(str);
		                return { "search_str": str };
		            },
		            "success": function (json) { 
		                //$.unblockUI();
		                //clear_search();
		            },
		            "error": function () {
		                $.unblockUI();
		                alert_error("Có lỗi kết nối đến server !!!");
		            }
		        }
		    },
		    // json_data
		    "json_data": {
		        "ajax": {
		            "url": tree_data.list_url,
		            "data": function (n) {
		                return { id: n.attr ? n.attr("id").replace("node_", "") + "_" + n.attr("level") : "", search_str: $("#search_text").val(), };
		                //"id": n.attr ? n.attr("id").replace("node_", "") : "", 
		                //"level": n.attr("level")
		            },
		            "error": function () {
		                alert_error(tree_data.list_url + ": Có lỗi liên kết đến server !!!");
		            }
		        }
		    },

		    // context menu
		    "contextmenu":
			{
			    "select_node": true,
			    "items": function (obj) {
			        var config = {};
			        var id = obj[0].id;
			        var class_name = tree_data.theme + "-context-menu";
			        // create
			        config["create"] = {
			            "label": "Thêm mới",
			            "separator_after": true,
			            "_class": class_name,
			            "icon": "images/add.png",
			            "action": function (obj) {
			                this.create(obj);
			            }
			        };
			        // rename
			        config["rename"] = {
			            // The item label
			            "label": "Sửa",
			            // All below are optional 
			            //"_disabled"			: true,		// clicking the item won't do a thing
			            "_class": class_name, // class is applied to the item LI node
			            //"separator_before"	: false,	// Insert a separator before the item
			            "separator_after": true, 	// Insert a separator after the item
			            // false or string - if does not contain `/` - used as classname
			            "icon": "images/edit.png",
			            //"submenu"			: { 
			            /* Collection of objects (the same structure) */
			            //}
			            // The function to execute upon a click
			            "action": function (obj) {
			                this.rename(obj);
			            }
			        };

			        // remove
			        config["remove"] = {
			            "label": "Xóa",
			            "separator_after": false,
			            "_class": class_name,
			            "icon": "images/cancel.png",
			            "action":
								function (obj) {
								    if (this.is_selected(obj))
								        this.remove();
								    else
								        this.remove(obj);
								}
			        };

			        // only show delete function to leaf node
			        if ($("#" + id).hasClass("jstree-leaf"))
			            config["remove"]["_disabled"] = false;
			        else
			            config["remove"]["_disabled"] = true;

			        // khong hien "them moi" cho node có level 3
			        if ($("#" + id).attr("level") == "3" || $("#" + id).attr("level") == 3)
			            config["create"]["_disabled"] = true;
			        else
			            config["create"]["_disabled"] = false;

			        // đổi tên "thêm mới" cho các node level 1, 2
			        if ($("#" + id).attr("level") == "1" || $("#" + id).attr("level") == 1)
			            config["create"]["label"] = "Thêm mới huyện, quận, ...";
			        if ($("#" + id).attr("level") == "2" || $("#" + id).attr("level") == 2)
			            config["create"]["label"] = "Thêm mới xã, phường, ...";

			        //Check phan quyen
			        if (delete_deny) config["remove"]["_disabled"] = true;
			        if (create_deny) config["create"]["_disabled"] = true;
			        if (edit_deny) config["rename"]["_disabled"] = true;

			        return config;
			    }
			},

		    // unique
		    //"unique" : 
		    //{ 
		    //	"error_callback" : function (n, p, f) 
		    //		{
		    //			
		    //			var currentTree = $.jstree._focused();
		    //			var new_node_string = currentTree._get_string("new_node").toLowerCase();
		    //			// neu khong phai la tao mot node moi mac dinh thi hien thong bao
		    //			if (n[0].toLowerCase() != new_node_string)
		    //			{		    								
		    //				//alert_error("Đã tồn tại loại khiếu tố '" + n + "' !!!" + f);
		    //			}
		    //			// khong thi focus den node do va chuyen sang che do edit
		    //			else
		    //			{
		    //				var i = 0;
		    //				for (i = 0; i < p.length; i++)
		    //				{
		    //					var aNode = p[i];
		    //					if (currentTree.get_text(aNode).toLowerCase() == new_node_string)
		    //					{
		    //						currentTree.rename(aNode);
		    //						break;
		    //					}
		    //				}
		    //			}
		    //		}
		    //},

		    // crm
		    "crm": {
		        "input_width_limit": 250
		    },

		    // core
		    "core": {
		        "animation": 500,
		        "strings": {
		            loading: "Đang tải dữ liệu ...",
		            new_node: "--Loại mới--",
		            multiple_selection: "Nhiều lựu chọn"
		        }
		    },

		    // ui
		    "ui": {
		        "select_limit": 1,
		        "selected_parent_close": "deselect"
		    },

		    // theme
		    "themes": {
		        "theme": tree_data.theme,
		        "dots": true,
		        "icons": true
		    }

		})
	// handle double click
		.bind("dblclick.jstree", function (event, data) {
		    var node = $(event.target).closest("li");
		    $(tree_id).jstree("toggle_node", node);
		})
	// handle create
		.bind("create.jstree", function (e, data) {
		    alert(tree_data.add_url);
		    $.ajax({
		        url: tree_data.add_url,
		        type: "POST",
		        data: {
		            "operation": "create_node",
		            "parent_id": data.rslt.parent === -1 ? "" : data.rslt.parent.attr("id").replace("node_", ""),
		            "name": data.rslt.name
		        },
		        dataType: "json",
		        success: function (r) {
		            if (r.status) {
		                // assign new id
		                $(data.rslt.obj).attr("id", "node_" + r.id);
		                // refresh parent node
		                data.inst.refresh(data.rslt.parent);
		                // select the new created node
		                //data.inst.select_node(data.inst._get_node(data.rslt.obj));
		            }
		            else {
		                // alert error
		                alert_error(r.message);
		                // rollback
		                $.jstree.rollback(data.rlbk);
		                //refreshTree();
		            }
		        },
		        error: function () {
		            alert_error("Có lỗi Server !!!");
		            $.jstree.rollback(data.rlbk);
		            //refreshTree();
		        }
		    });
		})
	//handle edit
		.bind("rename.jstree", function (e, data) {
		    $.ajax({
		        url: tree_data.edit_url,
		        type: "POST",
		        data: {
		            "operation": "edit_node",
		            "id": data.rslt.obj.attr("id").replace("node_", ""),
		            //"level": data.rslt.obj.attr("level"),
		            "name": data.rslt.new_name
		        },
		        dataType: "json",
		        success: function (r) {
		            if (!r.status) {
		                alert_error(r.message);
		                $.jstree.rollback(data.rlbk);
		                //refreshTree();
		            }
		            else {
		                data.inst.refresh(data.rslt.parent);
		            }
		        },
		        error: function () {
		            alert_error("Có lỗi Server !!!");
		            $.jstree.rollback(data.rlbk);
		            //refreshTree();
		        }
		    });
		})
	//handle move node
		.bind("move_node.jstree", function (e, data) {
		    data.rslt.o.each(function (i) {
		        $.ajax({
		            async: false,
		            type: 'POST',
		            url: tree_data.move_url,
		            dataType: "json",
		            data: {
		                "operation": "move_node",
		                "id": $(this).attr("id").replace("node_", ""),
		                "parent_id": data.rslt.cr === -1 ? "" : data.rslt.np.attr("id").replace("node_", "")
		            },
		            success: function (r) {
		                if (!r.status) {
		                    alert_error(r.message);
		                    $.jstree.rollback(data.rlbk);
		                    //refreshTree();
		                }
		                else {
		                    $(data.rslt.oc).attr("id", "node_" + r.id);
		                    if (data.rslt.cy && $(data.rslt.oc).children("UL").length) {
		                        data.inst.refresh(data.inst._get_parent(data.rslt.oc));
		                    }
		                    //refreshTree();
		                }
		            },
		            error: function () {
		                alert_error("Có lỗi Server !!!");
		                $.jstree.rollback(data.rlbk);
		                //refreshTree();
		            }
		        });
		    });
		})
	// handle delete
		.bind("remove.jstree", function (e, data) {
		    // neu khong phai la node leaf thi khong cho xoa
		    var o = data.inst.data.ui.last_selected || -1;
		    var class_t = data.rslt.obj[0].attributes["class"].value;
		    if (class_t.indexOf("jstree-leaf") == -1) {
		        alert_error("Chỉ cho phép xóa những node không có node con !!!");
		        $.jstree.rollback(data.rlbk);
		        // select lai node ban dau
		        //data.inst.deselect_all();
		        data.inst.deselect_node(data.inst._get_node(o));
		        data.inst.select_node(data.inst._get_next(o));
		        data.inst.set_focus();
		        return false;
		    }
		    
		    var idd = data.rslt.obj.attr("id").replace("node_", "");
		    data.rslt.obj.each(function () {   
		        showDelelePopup(idd, this.getAttribute("level"));
		    });
		    // confirm
		    //confirm_show(
			//	"Bạn có muốn xóa lựa chọn này?",
	    	//    {
	    	//        buttons: {
	    	//            "KHÔNG": function () {
	    	//                $.jstree.rollback(data.rlbk);
	    	//                // select lai node ban dau
	    	//                //data.inst.deselect_all();
	    	//                data.inst.deselect_node(data.inst._get_node(o));
	    	//                data.inst.select_node(data.inst._get_next(o));
	    	//                data.inst.set_focus();

	    	//                $(this).dialog("close");
	    	//                return false;
	    	//            },
	    	//            "CÓ": function () {
	    	//                data.rslt.obj.each(function () {
	    	//                    $.ajax({
	    	//                        async: false,
	    	//                        type: 'POST',
	    	//                        url: tree_data.delete_url,
	    	//                        dataType: "json",
	    	//                        data: {
	    	//                            "operation": "remove_node",
	    	//                            "id": this.id.replace("node_", ""),
	    	//                            "level": this.getAttribute("level")
	    	//                        },
	    	//                        success: function (r) {
	    	//                            if (!r.status) {
	    	//                                alert_error(r.message);
	    	//                                data.inst.refresh();
	    	//                            }
	    	//                            else {
	    	//                                alert_message("Đã xóa thành công.");
	    	//                            }
	    	//                        },
	    	//                        error: function () {
	    	//                            alert_error("Danh mục đang sử dụng không thể xóa ");
	    	//                            $.jstree.rollback(data.rlbk);
	    	//                            //refreshTree();
	    	//                        }
	    	//                    });
	    	//                });

	    	//                $(this).dialog("close");
	    	//                return false;
	    	//            }
	    	//        }
	    	//    }
	    	//);

		    // confirm
		    //			confirm_gosol(
		    //					"Bạn có muốn xóa lựa chọn này?",
		    //					function(result){
		    //						if (!result)
		    //						{
		    //							$.jstree.rollback(data.rlbk);
		    //							// select lai node ban dau
		    //							//data.inst.deselect_all();
		    //							data.inst.deselect_node(data.inst._get_node(o));
		    //							data.inst.select_node(data.inst._get_next(o));
		    //							data.inst.set_focus();
		    //							return false;
		    //						}
		    //						else
		    //						{
		    //							data.rslt.obj.each(function () {
		    //								$.ajax({
		    //									async : false,
		    //									type: 'POST',
		    //									url: tree_data.delete_url,
		    //									dataType: "json",
		    //									data : { 
		    //										"operation" : "remove_node", 
		    //										"id" : this.id.replace("node_","")
		    //									}, 
		    //									success : function (r) {
		    //										if(!r.status) {
		    //											alert_error(r.message);
		    //											data.inst.refresh();
		    //										}
		    //									},
		    //									error: function() {
		    //										alert_error("Có lỗi Server !!!");
		    //										$.jstree.rollback(data.rlbk);
		    //										//refreshTree();
		    //									}
		    //								});
		    //							});
		    //						}
		    //					}
		    //			)
		})
		.bind("before.jstree", function (e, data) {
		    if (show_edit_box) {
		        if (data.func === "rename") {
		            var id = data.args[0].attr("id").replace("node_", "");
		            var level = data.args[0].attr("level");
		            showEditForm(id, level);
		            e.stopImmediatePropagation();
		            return false;
		        }

		        if (data.func === "create") {
		            if (data.args[0] == -1)
		                showAddForm();
		            else {
		                var parentId = data.args[0].attr("id").replace("node_", "");
		                var level = data.args[0].attr("level");
		                fetchParentAddForm(parentId, level);
		            }
		            e.stopImmediatePropagation();
		            return false;
		        }
		    }
		})
		;
}