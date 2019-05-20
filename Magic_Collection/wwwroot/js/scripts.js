$().ready(function() {
    

    $(".draggable").draggable({revert: true});

    $(".droppable").droppable({
        classes: {"ui-droppable-hover" : "drop-hover"}, tolerance: "touch",
        drop: function(){
            console.log("dropped on target");
            $.ajax({
                url: '/CardsController.cs/Ajax',
                type: 'GET',
                contentType: false,
                // data: {test: "test"},
                success: function(valid)
                {
                    if(valid) {
                        console.log("Success");
                    }else{
                        console.log("Fail");
                    }
                }
            })
    }});
});