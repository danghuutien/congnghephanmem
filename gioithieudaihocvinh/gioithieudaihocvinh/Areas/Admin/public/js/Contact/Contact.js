
var vm = new Vue({
    el: '#demo',
    data: {
        datatb: {

            // đường dẫn đến ajax
            url: '/Admin/Contact/Ajax',
            // Số bản ghi trên 1 trang
            length: 10,

            // Biến tìm kiếm
            searchnow: '',
            // Số trang
            total: '',
            // Dữ liệu danh sách bảng
            tableData: [],
            // Trang hiện tại đang ở
            paginatenow: 1,
        },
        rowId: '',
        statusForm: '',
        dataForm: form({
            Title: '',
            FullName: '',
            Gmail: '',
            Message: ''
        })
            .rules({
                FullName: 'required',
                Gmail: 'required',

                /*test: 'required',*/

            })
            .messages({
                'FullName.required': 'Trường bắt buộc nhập',
                'Gmail.required': 'Trường bắt buộc nhập',



            }),

        isActivemodal: true,
        
        

    },
    watch: {
        





    },
    mounted: function () {
        this.loadData();
        const self = this;

        
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

        
        openmodal() {
            this.isActivemodal = false;
        },

        closemodal() {
            this.isActivemodal = true;
            this.dataForm.errors().messages = {};
        },
        saveform() {
           

            this.dataForm.Message = ""
            this.dataForm.Title = '';
            this.dataForm.FullName = '';
            this.dataForm.Gmail = '';
            



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
                axios.post("/Admin/Contact/Create", self.dataForm.data).then(function (response) {
                    self.thongbaothanhcong('Lưu thành công')
                    self.loadData();
                })
                    .catch(error => {
                        self.thongbaothatbai(error);
                    });
            } else {
                console.log(self.rowId)
                axios.post("/Admin/Contact/Edit",
                    {
                        id: self.rowId,
                        Title: self.dataForm.data.Title,
                        Message: self.dataForm.data.Message,
                        FullName: self.dataForm.data.FullName,
                        Gmail: self.dataForm.data.Gmail,
                     
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
            self.dataForm.data.Title = data.Title;
            self.dataForm.data.Message = data.Message;
            self.dataForm.data.FullName = data.FullName;
            self.dataForm.data.Gmail = data.Gmail;
            




            

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
                axios.post("/Admin/Contact/Delete", data)
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
