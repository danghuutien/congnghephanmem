
var vm = new Vue({
    el: '#demo',
    data: {
        datatb: {

            // đường dẫn đến ajax
            url: '/Home/AjaxComment',
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
        dataForm: form({
            Created_by: '',
            /*Thumbnail: '',*/
            Content: '',
            CatId:0
            
        })
            .rules({
                
                Created_by: 'required',
                
                Content: 'required',
                

                /*test: 'required',*/

            })
            .messages({
                'Created_by.required': 'Trường bắt buộc nhập',
                'Content.required': 'Trường bắt buộc nhập',



            }),

        isActivemodal: true,
        Id: 0,
        comment:0,
        
    },
    watch: {
        titlename(data) {
            this.dataForm.Title = data;
            this.ChangeToSlug(data);
            console.log(this.dataForm.data.name)
        },





    },
    mounted: function () {
        const self = this;
        this.statusForm = "insert";
        let uri = window.location.href.split("\/");
        let temp = uri[uri.length - 1];
        this.dataForm.CatId =temp[0];

        console.log(this.dataForm.CatId)
        this.loadData();
    },
    methods: {
        handleFile(e) {
            const self = this;
            let File = this.$refs.files.files[0];

            /*Initialize the form data*/

            let formData = new FormData();


            formData.append("files", File);

            console.log(formData)

            axios
                .post("/Admin/Post/UploadFile", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                })
                .then(function (response) {
                    if (response.status) {
                        self.thongbaothanhcong('Tải file thành công')

                    } else {
                        self.$notify.error({
                            title: 'Tải file thất bại',
                            message: 'Tải file thất bại',
                        });
                    }
                    /*self.loadfile();*/
                    // self.$toasted.success("Tải file thành công");
                })
                .catch(function () {
                    self.$notify.error({
                        title: 'Tải file thất bại',
                        /*message: text*/
                    });
                });
        },
        onChange(event) {
            // Gán lại trang hiện tại là 1 và gán lại giá trị searchnow ( biến dùng để tìm kiếm)
            /*this.datatb.paginatenow = 1;*/
            this.datatb.searchnow = event.target.value;
            /*console.log(this.datatb.searchnow)*/
            // Load lại bảng
            this.loadData();
        },

        
        openmodal() {
            this.isActivemodal = false;
        },

        closemodal() {
            this.isActivemodal = true;
            this.dataForm.errors().messages = {};
        },
        saveform() {
            this.dataForm.Created_by = ""
            this.dataForm.Thumbnail = '';
            this.dataForm.Content = '';



            this.statusForm = "insert";
            /*this.openmodal();*/
        },
        submitform() {
            /*console.log(1)*/
            if (this.dataForm.validate().errors().any()) {
                return;
            }
            const self = this;
            
            /*console.log(self.dataForm.data);*/

            
                axios.post("/Admin/Comment/Create", self.dataForm.data).then(function (response) {
                    self.thongbaothanhcong('Lưu thành công')
                    
                    self.loadData();
                })
                    .catch(error => {
                        self.thongbaothatbai(error);
                    });
           

        },
        pagination(data) {
            // Gán lại giá trị paginatenow
            this.datatb.paginatenow = data;
            // Load lại bảng
            this.loadData();
        },
        // load lại dữ liệu
        loadData() {
            console.log(this.dataForm.CatId)
            this.saveform()
            /*console.log(this.datatb.searchnow)*/
            const self = this;
            // Lấy index bản ghi bắt đầu
            var start = this.datatb.length * (this.datatb.paginatenow - 1);
            self.datatb.start = start;
            // Ajax dữ liệu
            axios.post(self.datatb.url , {
                start: this.datatb.start,
                length: this.datatb.length,
                searchnow: this.datatb.searchnow,
                CatId: this.dataForm.CatId
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
                    self.datatb.total = Math.ceil(
                        response.data.recordsTotal / self.datatb.length
                    );
                    // Dữ liệu bảng
                    self.datatb.tableData = response.data;
                    self.comment = response.data.length;
                    



                });
        },
        //data table
        doAlertEdit(data) {
            const self = this;
            // Gán giá trị cho form
            /*this.$refs.files.files[0] = null*/
            self.dataForm.data.name = data.Title;
            self.dataForm.data.Slug = data.Slug;
            self.dataForm.data.CatId = data.CatId;
            /*self.dataForm.data.Thumbnail = data.Thumbnail;*/
            self.dataForm.data.Excerpt = data.Excerpt;
            self.dataForm.data.Content = data.Content;
            self.titlename = data.Title


            /*console.log(this.titlename)*/

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
                axios.post("/Admin/Post/Delete", data)
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
