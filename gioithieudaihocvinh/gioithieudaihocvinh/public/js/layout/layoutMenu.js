var vm = new Vue({
    el: '#navbar',
    data: {
        datatb: {

            // đường dẫn đến ajax
            url: '/Admin/Post/Ajax',
            // Số bản ghi trên 1 trang
            length: 10,

            // Biến tìm kiếm
            searchnow: '',
            // Số trang
            total: '',
            // Dữ liệu danh sách bảng
            tableData: [],
            // tổng số trang hiện có
            total: '',
            // Trang hiện tại đang ở
            paginatenow: 1,
        },
        rowId: '',
        statusForm: '',
        /*dataForm: form({
            Title: '',
            Slug: '',
            CatId: '',
            Thumbnail: '',
            Excerpt: '',
            Content: ''
        })
            .rules({
                Title: 'required',
                Slug: 'required',
                CatId: 'required',

                *//*test: 'required',*//*

            })
            .messages({
                'Title.required': 'Trường bắt buộc nhập',
                'Slug.required': 'Trường bắt buộc nhập',
                'CatId.required': 'Trường bắt buộc nhập',



            }),*/

        isActivemodal: true,
        titlename: '',

        listDanhmuc: [],
        lisTypeCategory: [],

    },
    watch: {
        titlename(data) {
            this.dataForm.Title = data;
            this.ChangeToSlug(data);
            console.log(this.dataForm.data.name)
        },





    },
    mounted: async function () {
        /*this.loadData();*/
        const self = this;

        const res = await axios.get("/Home/GetDanhmuc")
            
         self.listDanhmuc = res.data
            
        self.listDanhmuc.forEach((menu) => {
            if (menu.Category.Typecategory_id == "2") {
                menu.Category["src"] = "/Home/TinTuc/" + menu.Category.Id
                if (menu.Kiemtra == 1) {
                    /*console.log(1)*/
                    menu.Childrens.forEach((children) => {
                        if (children.Typecategory_id == "2") {
                            children["src"] = "/Home/TinTuc/" + children.Id
                            /*console.log(1)*/
                        }
                       
                    })
                }

                
            }

            if (menu.Category.Typecategory_id == 3) {
                menu.Category.src = "/Home/LienHe/" + menu.Category.Slug
               

                
            }

        })


        /*console.log(self.listDanhmuc)*/

        axios.get("/Admin/Menu/GetTypeCategory")
            .then((res) => {
                self.lisTypeCategory = res.data
            })
    },
    methods: {
        
    }
})
