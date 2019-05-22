$().ready(function() {
    

    $(".draggable").draggable({
        start: function(){
            $(" #collectionValue").attr("value", $(this).attr("src"));
            $("#collectionDeleteValue").attr("value", $(this).attr("src"));
        },
        revert: true
    });


    $(".droppable").droppable({
        classes: {"ui-droppable-hover" : "drop-hover"}, tolerance: "touch",
        drop: function(){
            console.log("dropped on target");
            $("#collectionForm").submit();           
        }
    });

    $(".delete").droppable({
        classes: {"ui-droppable-hover" : "drop-hover"}, tolerance: "touch",
        drop: function(){
            console.log("dropped on target: deleting from collection");
            $("#collectionDeleteForm").submit();
        }
    });



    $("#pageInput").change(function(){
        $("#pageForm").attr("action", "/cards/search/"+$("#pageInput").val());
        $("#pageForm").submit();
    });


    $("#allPageInput").change(function(){
        $("#allPageForm").attr("action", "/cards/"+$("#allPageInput").val());
        $("#allPageForm").submit();
    });    
    

    

});