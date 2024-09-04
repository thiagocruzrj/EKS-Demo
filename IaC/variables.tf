# Generic Input Variables
variable "aws_region" {
  description = "Region in which AWS Resources to be created"
  type = string
  default = "us-east-1"
}

variable "business_divsion" {
  description = "Name or prefix of Business division."
  type        = string
}

variable "environment" {
  description = "Name or prefix of environment"
  type        = string
}