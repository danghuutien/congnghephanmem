
var vm = new Vue({
    el: '#demo',
    data: {
        datatb: {

            // đường dẫn đến ajax
            url: '/Admin/Background/Ajax',
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
            Title: '',
            
            CatId: '',
            Thumbnail: '',
            
        })
            .rules({
                Title: 'required',
                CatId: 'required',


                /*test: 'required',*/

            })
            .messages({
                'Title.required': 'Trường bắt buộc nhập',
                'CatId.required': 'Trường bắt buộc nhập',



            }),

        isActivemodal: true,
        titlename: '',
        
        listDanhmuc: [],
        lisTypeCategory: [],
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
            this.dataForm.Title = data;
            this.ChangeToSlug(data);
            console.log(this.dataForm.data.name)
        },





    },
    mounted: function () {
        this.loadData();
        const self = this;
        
        axios.get("/Admin/Menu/GetDanhmuc")
            .then((res) => {
                self.listDanhmuc = res.data
            })

        
    },
    methods: {
        handleThumbnail(e) {
            const self = this;
            let File = this.$refs.Thumbnail.files[0];

            /*Initialize the form data*/

            let formData = new FormData();


            formData.append("files", File);

            console.log(formData)

            axios
                .post("/Admin/Post/UploadThumbnail", formData, {
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
            
           
            this.dataForm.Title = '';
            this.dataForm.CatId = '';
            this.dataForm.Thumbnail = '';
            



            this.statusForm = "insert";
            this.openmodal();
        },
        submitform() {
            /*console.log(1)*/
            if (this.dataForm.validate().errors().any()) {
                return;
            }
            const self = this;
            this.closemodal()
            console.log(self.dataForm.data);

            if (this.statusForm == "insert") {
                axios.post("/Admin/Background/Create", self.dataForm.data).then(function (response) {
                    self.thongbaothanhcong('Lưu thành công')
                    self.loadData();
                })
                    .catch(error => {
                        self.thongbaothatbai(error);
                    });
            } else {
                console.log(self.rowId)
                axios.post("/Admin/Background/Edit",
                    {
                        id: self.rowId,
                        Title: self.dataForm.data.Title,
                        
                        CatId: self.dataForm.data.CatId,
                        Thumbnail: self.dataForm.data.Thumbnail,
                        





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
            this.datatb.paginatenow = data;
            // Load lại bảng
            this.loadData();
        },
        // load lại dữ liệu
        loadData() {
            console.log(this.datatb.searchnow)
            const self = this;
            // Lấy index bản ghi bắt đầu
            var start = this.datatb.length * (this.datatb.paginatenow - 1);
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
                    self.datatb.total = Math.ceil(
                        response.data.recordsTotal / self.datatb.length
                    );
                    // Dữ liệu bảng
                    self.datatb.tableData = response.data.data;



                });
        },
        //data table
        doAlertEdit(data) {
            const self = this;
            // Gán giá trị cho form
            /*this.$refs.files.files[0] = null*/
            self.dataForm.data.Title = data.Title;
           
            self.dataForm.data.CatId = data.CatId;
            


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
                axios.post("/Admin/Background/Delete", data)
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
