<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="订单编号" prop="codec">
				<el-input v-model="formData.codec" placeholder="请输入订单编号" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="订单金额" prop="sale_amt_text">
				<el-input v-model="formData.sale_amt_text" placeholder="请输入订单金额" :maxlength="11" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="成交金额" prop="paid_amt_text">
				<el-input v-model="formData.paid_amt_text" placeholder="请输入成交金额" :maxlength="11" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="订单备注" prop="remark">
				<el-input v-model="formData.remark" placeholder="请输入订单备注" :maxlength="256" show-word-limit clearable
					type="textarea" rows="5"></el-input>
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
				codec: [
					{ required: true, trigger: "blur", message: "请输入订单编号", },
				],
				sale_amt_text: [
					{ required: true, trigger: "blur", message: "请输入订单金额", },
				],
				paid_amt_text: [
					{ required: true, trigger: "blur", message: "请输入成交金额", },
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
				codes: '',
				codec: '',
				sale_amt_text: '0.00',
				paid_amt_text: '0.00',
				remark: '',

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.omssoheader.edit.get(row.id);
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
						res = await this.$API.omssoheader.add.post(this.formData);
					} else {
						res = await this.$API.omssoheader.update.put(this.formData);
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