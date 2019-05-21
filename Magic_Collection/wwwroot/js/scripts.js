$().ready(function() {
    

    $(".draggable").draggable({revert: true});

    $(".droppable").droppable({
        classes: {"ui-droppable-hover" : "drop-hover"}, tolerance: "touch",
        drop: function(){
            console.log("dropped on target");
            
            //TODO how to make this pass info or even just call a route on a controller?
        }
    });



    $("#pageInput").change(function(){
        $("#pageForm").attr("action", "/cards/"+$("#pageInput").val())
    });
});