
var vm = new Vue({
    el: '#demo',
    data: {
        datatb: {
            
            // đường dẫn đến ajax
            url: '/Admin/Menu/AjaxDanhmuc',
            // Số bản ghi trên 1 trang
            length: 10,

            // Biến tìm kiếm
            searchnow: '',
            // Số trang
            total: '',
            // Dữ liệu danh sách bảng
            
            // Trang hiện tại đang ở
            paginatenow: 1,
        },
        rowId: '',
        statusForm: '',
        dataForm: form({
            name: '',
            Slug: '',
            ParentId: '',
            Typecategory_id: ''
        })
            .rules({
                name: 'required',
                Typecategory_id: 'required',

            })
            .messages({
                'name.required': 'Trường bắt buộc nhập',
                'Typecategory_id.required': 'Trường bắt buộc nhập',
            }),

        isActivemodal: true,
        titlename: '',
        tableData: Array(),
        listDanhmuc: [],
        lisTypeCategory: [],
        data: {
            tableData: Array(),
            total: '',
            paginatenow: 1
        },
        normalizer(node) {


            return {
                id: node.Id,
                label: node.Name,
                children: node.Childrens,
            }


        },
       
    },
    watch: {
        titlename(data) {
            this.dataForm.name = data;
            this.ChangeToSlug(data);
            console.log(this.dataForm.data.name)
        },



    },
    mounted: function () {
        this.loadData();
        const self = this;

        axios.get("/Admin/Menu/GetDanhmuc1")
        .then((res) => {
            self.listDanhmuc = res.data
        console.log(self.listDanhmuc)
        })

        axios.get("/Admin/Menu/GetTypeCategory")
            .then((res) => {
                self.lisTypeCategory = res.data
                console.log(self.lisTypeCategory)
            })
    },
    methods: {
        onChange(event) {
            // Gán lại trang hiện tại là 1 và gán lại giá trị searchnow ( biến dùng để tìm kiếm)
            /*this.datatb.paginatenow = 1;*/
            this.datatb.searchnow = event.target.value;
            console.log(this.datatb.searchnow)
            // Load lại bảng
            this.loadData();
        },

        ChangeToSlug(title) {
            var slug;
            
            //Đổi chữ hoa thành chữ thường
            slug = title.toLowerCase();

            //Đổi ký tự có dấu thành không dấu
            slug = slug.replace(/(á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ)/, "a");
            slug = slug.replace(/(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)/, "e");
            slug = slug.replace(/(ì|í|ị|ỉ|ĩ)/, 'i');
            slug = slug.replace(/(ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ)/, 'o');
            slug = slug.replace(/(ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự)/, 'u');
            slug = slug.replace(/(ý|ỳ|ỷ|ỹ|ỵ)/, 'y');
            slug = slug.replace(/(đ)/, 'd');
            //Xóa các ký tự đặt biệt
            slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
            //Đổi khoảng trắng thành ký tự gạch ngang
            slug = slug.replace(/ /gi, "-");
            //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
            //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
            slug = slug.replace(/\-\-\-\-\-/gi, '-');
            slug = slug.replace(/\-\-\-\-/gi, '-');
            slug = slug.replace(/\-\-\-/gi, '-');
            slug = slug.replace(/\-\-/gi, '-');
            //Xóa các ký tự gạch ngang ở đầu và cuối
            slug = '@' + slug + '@';
            slug = slug.replace(/\@\-|\-\@|\@/gi, '');
            //In slug ra textbox có id “slug”
            this.dataForm.Slug = slug
        },
        openmodal() {
            this.isActivemodal = false;
        },

        closemodal() {
            this.isActivemodal = true;
            this.dataForm.errors().messages = {};
        },
        saveform() {
            this.titlename = ""
            this.dataForm.Slug = ""
            this.dataForm.name = '';
            this.dataForm.ParentId = '';
            this.dataForm.Typecategory_id = '';

            this.statusForm = "insert";
            this.openmodal();
        },
        submitform() {
            if (this.dataForm.validate().errors().any()) {
                return;
            }
            const self = this;
            this.closemodal()
            console.log(self.dataForm.data);
            
            if (this.statusForm == "insert") {
                axios.post("/Admin/Menu/CreateDanhmuc", self.dataForm.data).then(function (response) {
                    self.thongbaothanhcong('Lưu thành công')
                    self.loadData();
                })
                    .catch(error => {
                        self.thongbaothatbai(error);
                    });
            } else {
                console.log(self.rowId)
                axios.post("/Admin/Menu/EditDanhmuc",
                    {
                        id: self.rowId,
                        name: self.dataForm.data.name,
                        Slug: self.dataForm.data.Slug,
                        Typecategory_id: self.dataForm.data.Typecategory_id,
                        ParentId: self.dataForm.data.ParentId

                        
            
                    }
                    )
                    .then(function (response) {
                    self.thongbaothanhcong('Sửa thành công')
                    self.loadData();
                })
                    .catch(error => {
                        self.thongbaothatbai(error);
                    });
            }

        },
        pagination(data) {
            // Gán lại giá trị paginatenow
            this.data.paginatenow = data;
            // Load lại bảng
            this.loadData();
        },
        // load lại dữ liệu
        loadData() {
            console.log(this.datatb.searchnow)
            const self = this;
            // Lấy index bản ghi bắt đầu
            var start = this.datatb.length * (this.data.paginatenow - 1);
            self.datatb.start = start;
            // Ajax dữ liệu
            axios.post(self.datatb.url, {
                start: this.datatb.start,
                length: this.datatb.length,
                searchnow: this.datatb.searchnow,
                // Đẩy dữ liệu lên controller
                /*params: {
                    // Giá trị mặc định phải có
                    // start:index bản ghi bắt đầu
                    // length:số lượng bản ghi trên 1 trang
                    // searchcolum:Các cột được phép tìm kiếm
                    // searchnow: Giá trị tìm kiêm hiện tại

                    start: this.datatb.start,
                    length: this.datatb.length,
                    searchnow: this.searchnow,
                },*/
            })
                .then(function (response) {
                    // Tổng số trang hiện có
                    self.data.total = Math.ceil(
                        response.data.recordsTotal / 10
                    );
                    // Dữ liệu bảng
                    self.data.tableData = response.data.data;

                    

                });
        },
        //data table
        doAlertEdit(data) {
            const self = this;
            // Gán giá trị cho form
            self.dataForm.data.name = data.Name;
            self.dataForm.data.Slug = data.Slug;
            self.dataForm.data.Typecategory_id = data.Typecategory_id;
            self.dataForm.data.ParentId = data.ParentId;
            self.titlename = data.Name
            console.log(this.titlename)
            
            // Sửa tình trạng form
            this.statusForm = "update";
            this.rowId = data.Id;
            this.openmodal('sua');
        },
        doAlertDelete(data) {
            const self = this;
            this.$confirm('Thao tác này không thể quay lại, bạn chắc chắn tiếp tục?', 'Cảnh báo', {
                confirmButtonText: 'Vâng, xóa nó!',
                cancelButtonText: 'Không xóa!',
                type: 'warning',
                center: true
            }).then(() => {
                axios.post("/Admin/Menu/DeleteDanhmuc", data)
                    .then(function (response) {
                        self.loadData();
                        console.log(response.data.status)
                        if (response.data.status) {
                            self.$message({
                                type: 'success',
                                message: 'Xóa thành công'
                            });
                        } else {
                            self.thongbaothatbai(response.data[1])
                        }
                    })
                    .catch(function (error) {
                        // Thông báo xóa thất bại
                        self.thongbaothatbai(error)
                    });
                // this.$message({
                //     type: 'success',
                //     message: 'Xóa thành công'
                // });

            }).catch(() => {
                this.$message({
                    type: 'info',
                    message: 'Đã hủy xóa'
                });
            });


        },
        doAfterReload(data, table) {
            window.alert('data reloaded')
        },
        doCreating(comp, el) {
            console.log('creating')
        },
        doCreated(comp) {
            console.log('created')
        },
        doExport(type) {
            const parms = this.$refs.table.getServerParams()
            parms.export = type
            window.alert('GET /api/v1/export?' + jQuery.param(parms))
        },
        thongbaothanhcong(text) {
            this.$notify({
                title: 'Success',
                message: text,
                type: 'success'
            });
        },
        thongbaothatbai(text) {
            this.$notify.error({
                title: 'Error',
                message: text
            });

        },
    }
})
