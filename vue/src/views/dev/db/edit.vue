<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="名称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入名称" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="主机" prop="host">
				<el-input v-model="formData.host" placeholder="请输入主机" :maxlength="256" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="用户" prop="user">
				<el-input v-model="formData.user" placeholder="请输入用户" :maxlength="32" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="端口" prop="port">
				<el-input-number v-model="formData.port" placeholder="请输入端口" :maxlength="11" :max="65535"
					clearable></el-input-number>
			</el-form-item>
			<el-form-item label="数据库" prop="schame">
				<el-input v-model="formData.schame" placeholder="请输入数据库" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="字符集" prop="charset">
				<el-input v-model="formData.charset" placeholder="请输入字符集" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="显示排序" prop="od">
				<el-input-number v-model="formData.od" placeholder="请输入显示排序" :maxlength="11"></el-input-number>
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
			rules: {
				namec: [
					{ required: true, trigger: "blur", message: "名称不能为空" },
				],
				host: [
					{ required: true, trigger: "blur", message: "主机不能为空" },
				],
				schame: [
					{ required: true, trigger: "blur", message: "数据库不能为空" },
				],
				user: [
					{ required: true, trigger: "blur", message: "用户不能为空" },
				],
			},
		};
	},
	mounted() {
	},
	methods: {
		def_data() {
			return {
				id: '0',
				namec: '',
				host: '',
				port: 3306,
				user: '',
				schame: '',
				charset: 'utf8mb4',
				od: 0,
			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.devdb.edit.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.devdb.add.post(this.formData);
					} else {
						res = await this.$API.devdb.update.put(this.formData);
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

<style scoped>
.el-select {
	width: 100%;
}
</style>