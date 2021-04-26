Feature: Calculate Invoice Costs
	Invoice costs should be calculated from the parts, labour, MOT and VAT components of a job.
	VAT should not be applied to MOT costs.
	Historic changes to the rate of VAT should be reflected.
	Historic changes to the labour rate should be reflected.
	Historic changes to the MOT fee should be reflected.

Scenario: Calculate invoice and VAT costs for a job with parts, labour and MOT
	Given the current VAT rate is 20%
	And the current labour rate is £50.00 per hour
	And the current MOT fee is £45.00

	And the following job
	| Job Date   | MOT Test | Labour Hours |
	| 01/04/2021 | True     | 3.5          |
	And the following parts
	| Description | Cost  |
	| Air filter  | 15.50 |
	| Oil filter  | 7.50  |

	When the job invoice is calculated
	
	Then the job should have the following invoiced costs
	| Parts | Labour | VAT   | MOT   | Total  |
	| 23.00 | 175.00 | 39.60 | 45    | 282.60 |

Scenario: MOT fees should not attract VAT
	Given the current VAT rate is 20%
	And the current labour rate is £50.00 per hour
	And the current MOT fee is £45.00

	And the following job
	| Job Date   | MOT Test | Labour Hours |
	| 01/04/2021 | True     | 0            |
	
	When the job invoice is calculated
	
	Then the job should have the following invoiced costs
	| Parts | Labour | VAT  | MOT   | Total |
	| 0.00  | 0.00   | 0.00 | 45.00 | 45.00 |

Scenario: Historic VAT rates should be applied to historic jobs
	Given the following historic VAT rates
	| Applicable From | Rate |
	| 01/01/2010      | 17.5 |
	| 04/01/2011      | 20   |
	And the current labour rate is £50.00 per hour
	And the current MOT fee is £45.00

	And the following job
	| Job Date   | MOT Test | Labour Hours |
	| 01/04/2010 | False    | 0            |
	And the following parts
	| Description | Cost  |
	| Air filter  | 10.00 |
	
	When the job invoice is calculated
	
	Then the job should have the following invoiced costs
	| Parts | Labour | VAT  | MOT  | Total |
	| 10.00 | 0.00   | 1.75 | 0.00 | 11.75 |

Scenario: Historic labour rates should be applied to historic jobs
	Given the current VAT rate is 20%
	Given the following historic labour rates
	| Applicable From | Rate  |
	| 01/01/2020      | 45.00 |
	| 01/01/2021      | 50.00 |
	And the current MOT fee is £45.00
	
	And the following job
	| Job Date   | MOT Test | Labour Hours |
	| 01/04/2020 | False    | 1            |
	
	When the job invoice is calculated
	
	Then the job should have the following invoiced costs
	| Parts | Labour | VAT  | MOT   | Total |
	| 0.00  | 45.00  | 9.00 | 00.00 | 54.00 |

Scenario: Historic MOT fees should be applied to historic jobs
	Given the current VAT rate is 20%
	And the current labour rate is £50.00 per hour
	Given the following historic MOT fees
	| Applicable From | Fee   |
	| 01/01/2020      | 30.00 |
	| 01/01/2021      | 45.00 |

	And the following job
	| Job Date   | MOT Test | Labour Hours |
	| 01/04/2020 | True     | 0            |
	
	When the job invoice is calculated
	
	Then the job should have the following invoiced costs
	| Parts | Labour | VAT  | MOT   | Total |
	| 0.00  | 0.00   | 0.00 | 30.00 | 30.00 |
