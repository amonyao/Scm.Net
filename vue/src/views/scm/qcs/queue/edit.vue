<template>
	<sc-dialog v-model="visible" show-fullscreen :title="titleMap[mode]" width="700px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="方案" prop="detail_id">
				<sc-select v-model="formData.detail_id" placeholder="请选择方案" :data="detail_list" />
			</el-form-item>
			<el-form-item label="状态" prop="handle">
				<sc-select v-model="formData.handle" placeholder="请选择状态" :data="handle_list" />
			</el-form-item>
			<el-form-item label="姓名" prop="namec">
				<el-input v-model="formData.namec" clearable :maxlength="30" placeholder="请输入姓名" show-word-limit />
			</el-form-item>
			<el-form-item label="证件" prop="label">
				<el-input v-model="formData.label" clearable :maxlength="30" placeholder="请输入证件" show-word-limit />
			</el-form-item>
			<el-form-item label="电话" prop="phone">
				<el-input v-model="formData.phone" clearable :maxlength="30" placeholder="请输入电话" show-word-limit />
			</el-form-item>
			<el-form-item label="优先级" prop="lv">
				<el-input v-model="formData.lv" clearable :maxlength="30" placeholder="请输入优先级" show-word-limit />
			</el-form-item>
			<el-form-item label="备注" prop="remark">
				<el-input v-model="formData.remark" :autosize="{ minRows: 2, maxRows: 4 }" :maxlength="500"
					placeholder="请输入备注" show-word-limit type="textarea" />
			</el-form-item>
		</el-form>

		<template #footer>
			<el-button @click="close">取 消</el-button>
			<el-button :loading="isSaveing" type="primary" @click="save">
				确 定
			</el-button>
		</template>
	</sc-dialog>
</template>
<script>
export default {
	emits: ['complete'],
	data() {
		return {
			mode: "add",
			titleMap: { add: "新增", edit: "编辑", },
			isSaveing: false,
			visible: false,
			formData: this.def_data(),
			rules: {
				detail_id: [
					{ required: true, trigger: "change", pattern: this.$SCM.REGEX_ID, message: "请选择方案", },
				],
				handle: [
					{ required: true, trigger: "change", pattern: this.$SCM.REGEX_INT, message: "请选择状态", },
				],
			},
			detail_list: [this.$SCM.OPTION_ONE],
			handle_list: [this.$SCM.OPTION_ONE],
		};
	},
	mounted() {
		this.$SCM.list_dic(this.handle_list, 'qcs_handle', false);
		this.$SCM.list_option(this.detail_list, this.$API.scmqcsdetail.option, {}, false);
	},
	methods: {
		def_data() {
			return {
				id: '0',
				detail_id: '0',
				contact: '',
				label: '',
				phone: '',
				level: '0',
				handle: 0
			}
		},
		async open(row) {
			if (!row) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.scmqcsqueue.edit.get(row.id);
				this.formData = res.data;
				// this.formData.handle = '' + this.formData.handle;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.scmqcsqueue.add.post(this.formData);
					} else {
						res = await this.$API.scmqcsqueue.update.put(this.formData);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("操作成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				}
			});
		},
		close() {
			this.formData = this.def_data();
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>
