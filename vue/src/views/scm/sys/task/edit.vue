<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="" prop="codes">
				<el-input v-model="formData.codes" placeholder="请输入" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="" prop="names">
				<el-input v-model="formData.names" placeholder="请输入" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="" prop="types">
				<el-input v-model="formData.types" placeholder="请输入" :maxlength="4" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="" prop="start_time">
				<el-input v-model="formData.start_time" placeholder="请输入" :maxlength="24" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="" prop="end_time">
				<el-input v-model="formData.end_time" placeholder="请输入" :maxlength="24" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="" prop="elapsed_time">
				<el-input v-model="formData.elapsed_time" placeholder="请输入" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="" prop="status">
				<el-input v-model="formData.status" placeholder="请输入" :maxlength="4" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="" prop="row_delete">
				<el-input v-model="formData.row_delete" placeholder="请输入" :maxlength="4" show-word-limit
					clearable></el-input>
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
	data() {
		return {
			mode: "add",
			titleMap: { add: "新增", edit: "编辑" },
			visible: false,
			isSaveing: false,
			formData: this.def_data(),
			rules: {},
		};
	},
	mounted() {
	},
	methods: {
		def_data() {
			return {
				id: 0,
				codes: '',
				names: '',
				types: '',
				start_time: '',
				end_time: '',
				elapsed_time: '',
				status: '',
				row_delete: '',

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.systask.edit.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === 0) {
						res = await this.$API.systask.add.post(this.formData);
					} else {
						res = await this.$API.systask.update.put(this.formData);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("保存成功");
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

<style>
.el-select {
	width: 100%;
}
</style>