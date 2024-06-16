<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="分类" prop="cat_id">
				<el-input v-model="formData.cat_id" placeholder="请输入分类" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="商品类型" prop="types">
				<sc-select v-model="formData.types" placeholder="请输入商品类型"></sc-select>
			</el-form-item>
			<el-form-item label="商品编码" prop="codec">
				<el-input v-model="formData.codec" placeholder="请输入商品编码" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="商品名称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入商品名称" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="商品简称" prop="names">
				<el-input v-model="formData.names" placeholder="请输入商品简称" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="条码" prop="barcode">
				<el-input v-model="formData.barcode" placeholder="请输入条码" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="购买须知" prop="notice">
				<el-input v-model="formData.notice" placeholder="请输入购买须知" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="商品特色" prop="feature">
				<el-input v-model="formData.feature" placeholder="请输入商品特色" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="详细介绍" prop="desc">
				<el-input v-model="formData.desc" placeholder="请输入详细介绍" :maxlength="1024" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="展示状态" prop="show_status">
				<el-input v-model="formData.show_status" placeholder="请输入展示状态" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="销售状态" prop="sale_status">
				<el-input v-model="formData.sale_status" placeholder="请输入销售状态" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="售后状态" prop="ass">
				<el-input v-model="formData.ass" placeholder="请输入售后状态" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="热销商品" prop="hot">
				<el-input v-model="formData.hot" placeholder="请输入热销商品" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="会员商品" prop="vip">
				<el-input v-model="formData.vip" placeholder="请输入会员商品" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="推荐商品" prop="sss">
				<el-input v-model="formData.sss" placeholder="请输入推荐商品" :maxlength="4" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="已售数量" prop="qty">
				<el-input v-model="formData.qty" placeholder="请输入已售数量" :maxlength="11" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="显示排序" prop="od">
				<el-input v-model="formData.od" placeholder="请输入显示排序" :maxlength="11" show-word-limit
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
			rules: {
				codec: [
					{ required: true, trigger: "blur", message: "请输入商品编码" },
				],
				namec: [
					{ required: true, trigger: "blur", message: "请输入商品名称" },
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
				cat_id: '0',
				types: 0,
				codec: '',
				barcode: '',
				names: '',
				namec: '',
				notice: '',
				feature: '',
				desc: '',
				remark: '',
				show_status: 0,
				sale_status: 0,
				ass: 0,
				hot: 0,
				vip: 0,
				sss: 0,
				qty: 0,
				od: 0,
			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.posspu.edit.get(row.id);
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
						res = await this.$API.posspu.add.post(this.formData);
					} else {
						res = await this.$API.posspu.update.put(this.formData);
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