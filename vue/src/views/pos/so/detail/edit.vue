<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="" prop="order_id"> 
	<el-input 
		v-model="formData.order_id" 
		placeholder="请输入" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="显示排序" prop="od"> 
	<el-input 
		v-model="formData.od" 
		placeholder="请输入显示排序" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="商品ID" prop="spu_id"> 
	<el-input 
		v-model="formData.spu_id" 
		placeholder="请输入商品ID" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="规格ID" prop="spec_id"> 
	<el-input 
		v-model="formData.spec_id" 
		placeholder="请输入规格ID" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="扩展属性" prop="exts"> 
	<el-input 
		v-model="formData.exts" 
		placeholder="请输入扩展属性" 
		:maxlength="512" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="需求数量" prop="need_qty"> 
	<el-input 
		v-model="formData.need_qty" 
		placeholder="请输入需求数量" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="实际数量" prop="real_qty"> 
	<el-input 
		v-model="formData.real_qty" 
		placeholder="请输入实际数量" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="单价" prop="price"> 
	<el-input 
		v-model="formData.price" 
		placeholder="请输入单价" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="小计" prop="total"> 
	<el-input 
		v-model="formData.total" 
		placeholder="请输入小计" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
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
			titleMap: {add: "新增",edit: "编辑"},
			visible: false,
			isSaveing: false,
			formData: this.def_data(),
			rules: {},
		};
	},
	mounted() {
	},
	methods: {
	    def_data(){
		    return {
				id: '0',
				order_id:'', 
od:'', 
spu_id:'', 
spec_id:'', 
exts:'', 
need_qty:'', 
real_qty:'', 
price:'', 
total:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.possoorderdetail.edit.get(row.id);
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
						res = await this.$API.possoorderdetail.add.post(this.formData);
					} else {
						res = await this.$API.possoorderdetail.update.put(this.formData);
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