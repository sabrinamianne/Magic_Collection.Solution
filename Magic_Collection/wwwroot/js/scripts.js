$().ready(function() {
    

    $(".draggable").draggable({revert: true});

    $(".droppable").droppable({
        classes: {"ui-droppable-hover" : "drop-hover"},
        drop: function(){
            console.log("dropped on target");
    }});
});

